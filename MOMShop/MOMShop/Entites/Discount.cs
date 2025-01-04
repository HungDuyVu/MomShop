using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
    }
}
