using AutoMapper;
using MOMShop.Dto.Collection;
using MOMShop.Dto.Product;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements.UserProductService
{
    public class UserCollectionService : IUserCollectionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserCollectionService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public APIResponse GetDetailCollection(int id)
        {
            var collection = _dbContext.Collections.FirstOrDefault(e => e.Id == id);
            var productCollection = _dbContext.ProductCollections.Where(e => e.CollectionId == collection.Id);
            var products = new List<ProductDto>();
            foreach (var item in productCollection)
            {
                var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId && !e.Deleted);
                var productDto = _mapper.Map<ProductDto>(product);
                var productImage = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == product.Id);
                productDto.ImageUrl = productImage.ImageUrl;
                products.Add(productDto);
            }

            var result = _mapper.Map<UserCollectionDto>(collection);
            result.Products = products;
            return new APIResponse(result, "ok");
        }

        public APIResponse GetUserCollection()
        {
            var result = _dbContext.Collections.Where(e => e.Status == CollectionStatus.HOAT_DONG && !e.Deleted).ToList();
            return new APIResponse(result, "ok");
        }
    }
}
