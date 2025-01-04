namespace MOMShop.Dto.Discount
{
    public class DiscountDto
    {
        public int? Id { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
    }
}
