using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;
using System;

namespace MOMShop.Dto.ReceiveOrder
{
    public class FilterReceiveOrderDto : PagingBase
    {
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
        [FromQuery(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }
        [FromQuery(Name = "intendedTime")]
        public DateTime? IntendedTime { get; set; }
    }
}
