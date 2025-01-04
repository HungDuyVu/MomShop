using MOMShop.Dto.Product;
using MOMShop.Dto.ProductDetail;
using System.Collections.Generic;

namespace MOMShop.Dto.Collection
{
    public class ViewCollectionDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 1. Hoạt động, 2. Không hoạt động, 3.Khóa
        /// </summary>
        public int Status { get; set; }

        public List<ViewProductCollectionDto> Products { get; set; }
    }

    public class ViewProductCollectionDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 1. Áo thun, 2. Áo Sơ mi, 3. Áo khoác, 4. Quần, 5 Phụ Kiện
        /// </summary>
        public int ProductType { get; set; }
        public float Price { get; set; }
        public int Status { get; set; }
        public string ImageUrl { get; set; }
    }
}
