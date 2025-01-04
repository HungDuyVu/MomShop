using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 1. Hoạt động, 2. Không hoạt động, 3.Khóa
        /// </summary>
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
