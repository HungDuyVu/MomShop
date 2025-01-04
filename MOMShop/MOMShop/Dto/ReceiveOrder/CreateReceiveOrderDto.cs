using System;
using System.Collections.Generic;

namespace MOMShop.Dto.ReceiveOrder
{
    public class CreateReceiveOrderDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string Supplier { get; set; }
        public string Receiver { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public List<CreateReceiveOrderDetailDto> Details { get; set; }
    }
    public class CreateReceiveOrderDetailDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
