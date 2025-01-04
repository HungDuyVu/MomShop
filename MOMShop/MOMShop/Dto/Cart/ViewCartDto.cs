namespace MOMShop.Dto.Cart
{
    public class ViewCartDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Size { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public int ProductType { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
    }
}
