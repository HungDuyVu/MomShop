using MOMShop.Utils.APIResponse;

namespace MOMShop.Services.Interfaces.UserService
{
    public interface IUserCollectionService
    {
        APIResponse GetUserCollection();
        APIResponse GetDetailCollection(int id);
    }
}
