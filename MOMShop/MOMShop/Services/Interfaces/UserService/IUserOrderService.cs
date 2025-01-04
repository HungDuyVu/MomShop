using MOMShop.Dto.Order;
using MOMShop.Dto.Order.User;
using MOMShop.Entites;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOMShop.Services.Interfaces.UserService
{
    public interface IUserOrderService
    {
        APIResponse Create(OrderDto order);
        OrderDto FindById(int id);
        List<ViewOrderDto> FindAll(FilterOrderDto input);

        void UpdateStatus(int id, int status);
        void Delete(int id);
    }
}
