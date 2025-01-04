using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;

namespace MOMShop.Dto.Product
{
    public class FilterProductDto : PagingBase
    {
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
        public int? ProductType { get; set; }
    }
}
