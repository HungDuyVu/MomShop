using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using MOMShop.Dto.ProductDetail;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using MOMShop.Utils.HistoryUpdate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace MOMShop.Services.Implements
{
    public class ProductServices : IProductServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductServices(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public APIResponse AddProducts(CreateProductDto input)
        {
            var check = _dbContext.Products.FirstOrDefault(e => e.Code == input.Code && !e.Deleted);
            if (check != null)
            {
                return new APIResponse("duplicate");
            }
            var insert = _mapper.Map<Product>(input);
            var result = _dbContext.Products.Add(insert);
            _dbContext.SaveChanges();

            foreach (var item in input.ProductDetails)
            {
                var insertDetail = new ProductDetail()
                {
                    ProductId = result.Entity.Id,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    Description = item.Description
                };
                _dbContext.ProductDetails.Add(insertDetail);
            }
            _dbContext.SaveChanges();
            return new APIResponse(_mapper.Map<ProductDto>(result.Entity), "ok");
        }

        public ProductDto UpdateProducts(UpdateProductDto input)
        {
            var product = _dbContext.Products.FirstOrDefault(e => e.Id == input.Id);
            if (product == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }

            if(product.Price != input.Price)
            {
                _dbContext.HistoryUpdates.Add(new HistoryUpdate()
                {
                    Table = HistoryUpdateTable.PRODUCT,
                    ReferId = product.Id,
                    ColumnUpdate = HistoryUpdateColumn.PRODUCT_PRICE,
                    OldValue = product.Price.ToString(),
                    NewValue = input.Price.ToString()
                });
            }

            product.Name = input.Name;
            product.Code = input.Code;
            product.Status = input.Status;
            product.Description = input.Description;
            product.ProductType = input.ProductType;

            if (input.ProductDetails.Count() == 0)
            {
                var removelList = _dbContext.ProductDetails.Where(e => e.ProductId == product.Id);
                _dbContext.ProductDetails.RemoveRange(removelList);
            }

            foreach (var item in input.ProductDetails)
            {
                var removelList = _dbContext.ProductDetails.Where(e => e.ProductId == product.Id && !input.ProductDetails.Select(d => d.Size).Contains(e.Size));
                _dbContext.ProductDetails.RemoveRange(removelList);
                var detail = _dbContext.ProductDetails.FirstOrDefault(e => e.Size == item.Size && e.ProductId == product.Id);
                if(detail != null)
                {
                    detail.Quantity = item.Quantity;
                    detail.Size = item.Size;
                    detail.Description = item.Description;
                } else
                {
                    var insertDetail = new ProductDetail()
                    {
                        ProductId = product.Id,
                        Size = item.Size,
                        Quantity = item.Quantity,
                        Description = item.Description
                    };
                    _dbContext.ProductDetails.Add(insertDetail);
                }
            }

            _dbContext.SaveChanges();
            return _mapper.Map<ProductDto>(product);
        }

        public void DeleteProducts(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(e => e.Id == id);
            if (product == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }
            var productDetail = _dbContext.ProductDetails.Where(e => e.ProductId == id);
            if (productDetail.Any())
            {
                _dbContext.ProductDetails.RemoveRange(productDetail);
            }
            var productImages = _dbContext.ProductImages.Where(e => e.ProductId == id);
            if (productImages.Any())
            {
                var baseDir = Directory.GetParent(Directory.GetParent(_hostEnvironment.ContentRootPath).FullName).FullName;
                foreach (var item in productImages)
                {
                    string folderPath = Path.Combine(baseDir + $"\\MOMShop\\images\\{product.Code}");
                    if (Directory.Exists(folderPath))
                    {
                        string url = item.ImageUrl;
                        int startIndex = url.LastIndexOf("=") + 1;
                        string fileName = url.Substring(startIndex);
                        string filePath = Path.Combine(folderPath, fileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            _dbContext.ProductImages.Remove(item);
                        }
                    }
                }
            }

            var productCollection = _dbContext.ProductCollections.Where(e => e.ProductId == id);
            _dbContext.ProductCollections.RemoveRange(productCollection);
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();
        }

        public List<ProductDetailDto> Details(int id)
        {
            var productDetail = _dbContext.ProductDetails.Where(e => e.ProductId == id);
            return _mapper.Map<List<ProductDetailDto>>(productDetail);
        }

        public ProductDto FindById(int id)
        {
            var result = new ProductDto();
            var product = _dbContext.Products.FirstOrDefault(e => e.Id == id && !e.Deleted);
            var productDetails = _dbContext.ProductDetails.Where(e => e.ProductId == product.Id).ToList();
            result = _mapper.Map<ProductDto>(product);
            result.ProductDetails = _mapper.Map<List<ProductDetailDto>>(productDetails);
            var image = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == id);
            if (image != null)
            {
                result.ImageUrl = image.ImageUrl;
            }
            return result;
        }

        public Paging<ProductDto> GetProducts(FilterProductDto input)
        {
            var result = new Paging<ProductDto>();
            result.Items = new List<ProductDto>();

            var products = _dbContext.Products.Where(e => !e.Deleted && (input.Status == null || e.Status == input.Status) && (input.Keyword == null || e.Code.Contains(input.Keyword) || e.Name.Contains(input.Keyword))).OrderByDescending(e => e.Id).ToList();

            foreach (var product in products)
            {
                var item = _mapper.Map<ProductDto>(product);
                var productDetails = _dbContext.ProductDetails.Where(e => e.ProductId == product.Id).ToList();
                item.ProductDetails = _mapper.Map<List<ProductDetailDto>>(productDetails);
                result.Items.Add(item);
                var image = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == item.Id);
                if(image != null)
                {
                    item.ImageUrl = image.ImageUrl;
                }
            }
            result.TotalItems = result.Items.Count;
            return result;
        }

        public void AddProductImage(IFormFile input, int productId)
        {
            var product = _dbContext.Products.FirstOrDefault(e => e.Id == productId);
            if(product != null)
            {
                var baseDir = Directory.GetParent(Directory.GetParent(_hostEnvironment.ContentRootPath).FullName).FullName;
                var productImage = _dbContext.ProductImages.Where(e => e.ProductId == product.Id);
                foreach (var item in productImage)
                {
                    string folderPath = Path.Combine(baseDir + $"\\MOMShop\\images\\{product.Code}");
                    if (Directory.Exists(folderPath))
                    {
                        string url = item.ImageUrl;
                        int startIndex = url.LastIndexOf("=") + 1;
                        string fileName = url.Substring(startIndex);
                        string filePath = Path.Combine(folderPath, fileName);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                            _dbContext.ProductImages.Remove(item);
                        }
                    }
                }
            }

            if (input != null)
            {
                var baseDir = Directory.GetParent(Directory.GetParent(_hostEnvironment.ContentRootPath).FullName).FullName;
                string folderPath = Path.Combine(baseDir + $"\\MOMShop\\images\\{product.Code}");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string fileName = input.FileName;

                string filePath = Path.Combine(baseDir + $"\\MOMShop\\images\\{product.Code}", fileName);

                var endpoint = $"api/file/get?folder={product.Code}&fileName={fileName}";

                using (var filestream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    input.CopyTo(filestream);
                }

                var data = new ProductImage()
                {
                    ProductId = productId,
                    ImageUrl = endpoint
                };

                _dbContext.ProductImages.Add(data);
                _dbContext.SaveChanges();
            }
        }
    }
}
