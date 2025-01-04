using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
