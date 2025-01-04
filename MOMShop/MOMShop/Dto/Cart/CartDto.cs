namespace MOMShop.Dto.Cart
{
    public class CartDto
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public int? CustomerId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}
