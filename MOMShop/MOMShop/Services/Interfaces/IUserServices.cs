using MOMShop.Dto.Users;
using MOMShop.Utils.APIResponse;

namespace MOMShop.Services.Interfaces
{
    public interface IUserServices
    {
        UserDto Login(LoginDto input);
        UserDto FindById(int id);
        APIResponse Register(RegisterDto input);
        string UpdateInforUser(UserDto input);
    }
}
