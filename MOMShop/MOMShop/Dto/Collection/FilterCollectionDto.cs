using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;

namespace MOMShop.Dto.Collection
{
    public class FilterCollectionDto : PagingBase
    {
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
    }
}
