using MOMShop.Dto.Product;
using MOMShop.Utils;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces.UserService
{
    public interface IUserProductService
    {
        List<ProductDto> GetAllProductForUser(FilterProductDto input);
    }
}
