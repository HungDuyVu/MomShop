using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;
using System;

namespace MOMShop.Dto.Customer
{
    public class FilterCustomerDto : PagingBase
    {
        [FromQuery(Name = "status")]
        public int? Status { get; set; }
        [FromQuery(Name = "birthDate")]
        public DateTime? BirthDate { get; set; }
    }
}
