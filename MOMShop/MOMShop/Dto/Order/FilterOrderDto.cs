using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;
using System;
using System.Xml.Linq;

namespace MOMShop.Dto.Order
{
    public class FilterOrderDto : PagingBase
    {
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
        [FromQuery(Name = "orderCode")]
        public string OrderCode { get; set; }

        [FromQuery(Name = "customerId")]
        public int? CustomerId { get; set; }
        [FromQuery(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }
        [FromQuery(Name = "intendedTime")]
        public DateTime? IntendedTime { get; set; }
    }
}
