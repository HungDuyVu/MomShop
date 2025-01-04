using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;

namespace MOMShop.Dto.Discount
{
    public class FilterDiscountDto : PagingBase
    {
        [FromQuery(Name = "DiscountCode")]
        public string DiscountCode { get; set; }
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
    }
}
