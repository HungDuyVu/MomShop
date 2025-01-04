namespace MOMShop.Dto.Order
{
    public class OrderSendGHN
    {
        public int Id { get; set; }
        public string To_name { get; set; }
        public string To_phone { get; set; }
        public string To_address { get; set; }
        public string To_ward_code { get; set; }
        public int To_district_id { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string Seight { get; set; }
        public string Service_type_id { get; set; }
        public string Service_id { get; set; }
        public string Payment_type_id { get; set; }
        public string Required_note { get; set; }
        public object Items { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }
}
