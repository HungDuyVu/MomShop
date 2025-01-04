using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class CustomerFavourite
    {
        [Key]
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
    }
}
