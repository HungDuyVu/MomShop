using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MOMShop.Dto.Product
{
    public class CreateProductDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int ProductType { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public List<CreateProductDetailDto> ProductDetails { get; set; }

    }
    public class CreateProductDetailDto
    {
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
