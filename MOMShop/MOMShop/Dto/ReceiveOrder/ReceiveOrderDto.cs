using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using MOMShop.Dto.ReceiveOrderDetail;

namespace MOMShop.Dto.ReceiveOrder
{
    public class ReceiveOrderDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Supplier { get; set; }
        public string Receiver { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public float TotalMoney { get; set; }
        public List<ReceiveOrderDetailDto> Details { get; set; }    
    }
}
