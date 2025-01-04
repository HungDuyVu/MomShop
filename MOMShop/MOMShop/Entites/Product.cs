using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 1. Áo thun, 2. Áo Sơ mi, 3. Áo khoác, 4. Quần, 5 Phụ Kiện
        /// </summary>
        public int ProductType { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
