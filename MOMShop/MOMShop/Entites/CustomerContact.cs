using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class CustomerContact
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public bool? IsDefault { get; set; }
        public bool Deleted { get; set; }
    }
}
