using MOMShop.Dto.ProductDetail;
using System.Collections.Generic;

namespace MOMShop.Dto.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 1. Áo thun, 2. Áo Sơ mi, 3. Áo khoác, 4. Quần, 5 Phụ Kiện
        /// </summary>
        public int ProductType { get; set; }
        public string ProductTypeName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string ProductStatusName { get; set; }
        public bool Deleted { get; set; }
        public string ImageUrl { get; set; }
        public List<ProductDetailDto> ProductDetails { get; set; }
    }


}
