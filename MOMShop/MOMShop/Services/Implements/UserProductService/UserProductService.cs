using AutoMapper;
using MOMShop.Dto.Product;
using MOMShop.Dto.ProductDetail;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements.UserProductService
{
    public class UserProductService : IUserProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserProductService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<ProductDto> GetAllProductForUser(FilterProductDto input)
        {
            var result = new List<ProductDto>();

            var products = _dbContext.Products.Where(e => !e.Deleted 
            && (e.Status == Status.HOAT_DONG)
            && (input.Status == null || e.Status == input.Status) 
            && (input.ProductType == null || e.ProductType == input.ProductType)
            && (input.Keyword == null || e.Code.ToLower().Contains(input.Keyword.ToLower()) || e.Name.ToLower().Contains(input.Keyword.ToLower()))).OrderByDescending(e => e.Id).ToList();

            foreach (var product in products)
            {
                var item = _mapper.Map<ProductDto>(product);
                var productDetails = _dbContext.ProductDetails.Where(e => e.ProductId == product.Id).ToList();
                item.ProductDetails = _mapper.Map<List<ProductDetailDto>>(productDetails);
                result.Add(item);
                var image = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == item.Id);
                if (image != null)
                {
                    item.ImageUrl = image.ImageUrl;
                }
            }
            return result;
        }

    }
}
