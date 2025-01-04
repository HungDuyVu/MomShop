using MOMShop.Dto.Collection;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface ICollectionService
    {
        APIResponse Create(CollectionDto input);
        CollectionDto Update(CollectionDto input);
        void Delete(int id);
        CollectionDto FindById(int id, string keyword);
        ViewCollectionDto Find(int id, string keyword);
        List<CollectionDto> FindAll(FilterCollectionDto input);
    }
}
