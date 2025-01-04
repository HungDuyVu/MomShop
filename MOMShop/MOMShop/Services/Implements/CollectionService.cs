using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MOMShop.Dto.Collection;
using MOMShop.Dto.Product;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class CollectionService : ICollectionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CollectionService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public APIResponse Create(CollectionDto input)
        {
            var check = _dbContext.Collections.FirstOrDefault(e => e.Code == input.Code && !e.Deleted);
            if(check != null)
            {
                return new APIResponse("duplicate");
            }
            var insert = _mapper.Map<Collection>(input);
            var result = _dbContext.Collections.Add(insert);
            _dbContext.SaveChanges();
            var products = _dbContext.ProductCollections.Where(e => e.CollectionId == result.Entity.Id && !input.Products.Contains(e.ProductId)).ToList();
            foreach (var item in products)
            {
                _dbContext.ProductCollections.Remove(item);
            }
            foreach (var item in input.Products)
            {
                var productCollection = _dbContext.ProductCollections.FirstOrDefault(e => e.ProductId == item && e.CollectionId == result.Entity.Id);
                if (productCollection == null)
                {
                    _dbContext.ProductCollections.Add(new ProductCollection
                    {
                        CollectionId = result.Entity.Id,
                        ProductId= item,
                    });
                }
            }
            _dbContext.SaveChanges();
            return new APIResponse(_mapper.Map<CollectionDto>(result.Entity),"ok");
        }

        public void Delete(int id)
        {
            var collection = _dbContext.Collections.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (collection == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }
            collection.Deleted = true;
            _dbContext.SaveChanges();
        }

        public List<CollectionDto> FindAll(FilterCollectionDto input)
        {
            var collections = _dbContext.Collections.Where(e => !e.Deleted && (input.Status == null || e.Status == input.Status) 
            && (input.Keyword == null || e.Code.ToLower().Contains(input.Keyword) || e.Name.ToLower().Contains(input.Keyword))).OrderByDescending(e => e.Id).ToList();
            return _mapper.Map<List<CollectionDto>>(collections);
        }

        public CollectionDto FindById(int id, string keyword)
        {
            var collection = _dbContext.Collections.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (collection == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }
            var result = _mapper.Map<CollectionDto>(collection);
            var productCollection = _dbContext.ProductCollections.Where(e => e.CollectionId == collection.Id);
            if (productCollection.Any())
            {
                result.Products = productCollection.Select(e => e.ProductId).ToList();
            }
            return result;
        }
        public ViewCollectionDto Find(int id, string keyword)
        {
            var collection = _dbContext.Collections.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (collection == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }
            var result = _mapper.Map<ViewCollectionDto>(collection);
            var list = new List<ViewProductCollectionDto>();
            var productCollection = _dbContext.ProductCollections.Where(e => e.CollectionId == collection.Id).AsNoTracking().ToList();
            if (productCollection.Any())
            {
                foreach (var item in productCollection)
                {
                    var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId && !e.Deleted);
                    if (product != null)
                    {
                        var productItem = _mapper.Map<ViewProductCollectionDto>(product);
                        var productImage = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == product.Id);
                        if (productImage != null)
                        {
                            productItem.ImageUrl = productImage.ImageUrl;
                        }
                        list.Add(productItem);
                    }
                }
                list = list.Where(e => (keyword == null || e.Code.ToLower().Contains(keyword.ToLower()) || e.Name.ToLower().Contains(keyword.ToLower()))).ToList();
                result.Products = list;
            }
            return result;
        }

        public CollectionDto Update(CollectionDto input)
        {
            var collection = _dbContext.Collections.FirstOrDefault(e => e.Id == input.Id && !e.Deleted);
            if (collection == null)
            {
                throw new System.Exception("Không tìm thấy sản phẩm");
            }
            collection.Name = input.Name;
            collection.Description = input.Description;
            collection.Status = input.Status;

            var products = _dbContext.ProductCollections.Where(e => e.CollectionId == collection.Id && !input.Products.Contains(e.ProductId)).ToList();
            foreach (var item in products)
            {
                _dbContext.ProductCollections.Remove(item);
            }
            foreach (var item in input.Products)
            {
                var productCollection = _dbContext.ProductCollections.FirstOrDefault(e => e.ProductId == item && e.CollectionId == collection.Id);
                if (productCollection == null)
                {
                    _dbContext.ProductCollections.Add(new ProductCollection
                    {
                        CollectionId = collection.Id,
                        ProductId = item,
                    });
                }
            }
            _dbContext.SaveChanges();
            var result = _mapper.Map<CollectionDto>(collection);
            result.Products = input.Products;
            return result;
        }
    }
}
