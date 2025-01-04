namespace MOMShop.Entites
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
