using MOMShop.Dto.ProductDetail;

namespace MOMShop.Services.Interfaces
{
    public interface IProductDetailServices
    {
        ProductDetailDto AddProductDetail(ProductDetailDto input);
        ProductDetailDto UpdateProductDetail(ProductDetailDto input);
        ProductDetailDto FindDetailById(int id);
        void DeleteProductDetail(int id);
    }
}
