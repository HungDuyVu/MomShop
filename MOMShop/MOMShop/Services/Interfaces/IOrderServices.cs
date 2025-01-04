using MOMShop.Dto.Order;
using MOMShop.Utils;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface IOrderServices
    {
        List<OrderDto> GetAllOrder(FilterOrderDto input);
        OrderDto FindById(int id);
        void UpdateStatus(int id, int status);
        void Delete(int id);
        ViewOrderReciptDto ViewOrder(int id);
    }
}
