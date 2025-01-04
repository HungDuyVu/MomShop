using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class ReceiveOrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int ReceiveOrderId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
