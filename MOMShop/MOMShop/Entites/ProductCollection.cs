using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class ProductCollection
    {
        [Key]
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public int ProductId { get; set; }
    }
}
