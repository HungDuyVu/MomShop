using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDefault { get; set; } = true;
    }
}
