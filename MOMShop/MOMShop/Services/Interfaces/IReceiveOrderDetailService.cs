using MOMShop.Dto.ReceiveOrderDetail;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface IReceiveOrderDetailService
    {
        ReceiveOrderDetailDto Add(ReceiveOrderDetailDto input);
        ReceiveOrderDetailDto Update(ReceiveOrderDetailDto input);
        void Delete(int id);
        ReceiveOrderDetailDto FindById(int id);
        List<ReceiveOrderDetailDto> GetAll(int receiveOrderId);
    }
}
