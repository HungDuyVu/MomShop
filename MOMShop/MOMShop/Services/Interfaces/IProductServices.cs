using Microsoft.AspNetCore.Http;
using MOMShop.Dto.Product;
using MOMShop.Dto.ProductDetail;
using MOMShop.Entites;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface IProductServices
    {
        Paging<ProductDto> GetProducts(FilterProductDto input);
        APIResponse AddProducts(CreateProductDto input);
        List<ProductDetailDto> Details(int id);
        ProductDto FindById(int id);
        ProductDto UpdateProducts(UpdateProductDto input);
        void DeleteProducts(int id);

        void AddProductImage(IFormFile input, int productId);

    }
}
