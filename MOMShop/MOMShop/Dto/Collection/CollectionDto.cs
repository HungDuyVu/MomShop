using MOMShop.Dto.Product;
using System.Collections.Generic;

namespace MOMShop.Dto.Collection
{
    public class CollectionDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 1. Hoạt động, 2. Không hoạt động, 3.Khóa
        /// </summary>
        public int Status { get; set; }

        public List<int> Products { get; set; }
    }

    public class UserCollectionDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 1. Hoạt động, 2. Không hoạt động, 3.Khóa
        /// </summary>
        public int Status { get; set; }

        public List<ProductDto> Products { get; set; }

    }
}
