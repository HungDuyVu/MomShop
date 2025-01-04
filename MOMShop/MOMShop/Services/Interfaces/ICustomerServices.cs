using MOMShop.Dto.Customer;
using MOMShop.Dto.Users;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface ICustomerServices
    {
        List<UserDto> GetAll(FilterCustomerDto input);
        void ChangeStatus(int id);
        void Delete(int id);
        APIResponse Find(int id);
    }
}
