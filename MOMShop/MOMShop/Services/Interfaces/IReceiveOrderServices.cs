using MOMShop.Dto.ReceiveOrder;
using MOMShop.Utils;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface IReceiveOrderServices
    {
        ReceiveOrderDto Add(CreateReceiveOrderDto input);
        ReceiveOrderDto Update(CreateReceiveOrderDto input);
        void Delete(int id);
        ReceiveOrderDto FindById(int id);
        List<ReceiveOrderDto> GetReceiveOrders(FilterReceiveOrderDto input);
        void UpdateStatus(int id, int status);
    }
}
