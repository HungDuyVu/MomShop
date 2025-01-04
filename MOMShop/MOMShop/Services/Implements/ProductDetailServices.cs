using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOMShop.Dto.ProductDetail;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using System;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class ProductDetailServices : IProductDetailServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductDetailServices(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public ProductDetailDto AddProductDetail(ProductDetailDto input)
        {
            var insert = _mapper.Map<ProductDetail>(input);
            _dbContext.Add(insert);
            _dbContext.SaveChanges();
            return input;
        }

        public ProductDetailDto UpdateProductDetail(ProductDetailDto input)
        {
            var productDetail = _dbContext.ProductDetails.FirstOrDefault(e => e.Id == input.Id);
            if (productDetail == null)
            {
                throw new Exception("Không tìm thấy chi tiết sản phẩm");
            }
            productDetail.Size = input.Size;
            productDetail.Quantity = input.Quantity;
            productDetail.Description = input.Description;
            _dbContext.SaveChanges();
            return input;
        }

        public void DeleteProductDetail(int id)
        {
            var productDetail = _dbContext.ProductDetails.FirstOrDefault(e => e.Id == id);
            if (productDetail == null)
            {
                throw new Exception("Không tìm thấy chi tiết sản phẩm");
            }
            _dbContext.ProductDetails.Remove(productDetail);
            _dbContext.SaveChanges();
        }

        public ProductDetailDto FindDetailById(int id)
        {
            var productDetail = _dbContext.ProductDetails.FirstOrDefault(e => e.Id == id);
            if (productDetail == null)
            {
                throw new Exception("Không tìm thấy chi tiết sản phẩm");
            }
            return _mapper.Map<ProductDetailDto>(productDetail);
        }
    }
}
