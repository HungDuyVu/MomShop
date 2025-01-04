using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
