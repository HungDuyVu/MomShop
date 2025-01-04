using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using MOMShop.Dto.ProductDetail;
using MOMShop.Entites;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using System;
using System.Collections.Generic;

namespace MOMShop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServices _services;
        private IProductDetailServices _productDetailServices;
        public ProductController(IProductServices services, IProductDetailServices productDetailServices)
        {
            _services = services;
            _productDetailServices = productDetailServices;
        }

        [HttpGet("find-all")]
        public Paging<ProductDto> GetProducts([FromQuery]FilterProductDto input)
        {
            try
            {
                var result = _services.GetProducts(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public APIResponse AddProducts([FromBody] UpdateProductDto input)
        {
            try
            {
                var result = _services.AddProducts(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("upload-image")]
        public void AddProductImage(int productId, IFormFile input)
        {
            try
            {
                _services.AddProductImage(input, productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update")]
        public ProductDto UpdateProducts([FromBody] UpdateProductDto input)
        {
            try
            {
                var result = _services.UpdateProducts(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public void DeleteProduct(int id)
        {
            try
            {
                _services.DeleteProducts(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("detail/{id}")]
        public List<ProductDetailDto> ProductDetails(int id)
        {
            try
            {
                var result = _services.Details(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("find-by-id/{id}")]
        public ProductDto FindById(int id)
        {
            try
            {
                var result = _services.FindById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add-detail")]
        public ProductDetailDto AddProductDetails([FromBody] ProductDetailDto input)
        {
            try
            {
                var result = _productDetailServices.AddProductDetail(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update-detail")]
        public ProductDetailDto UpdateProductDetails([FromBody] ProductDetailDto input)
        {
            try
            {
                var result = _productDetailServices.UpdateProductDetail(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete-detail/{id}")]
        public void DeleteProducDetailt(int id)
        {
            try
            {
                _productDetailServices.DeleteProductDetail(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("product-detail/{id}")]
        public ProductDetailDto FindDetailById(int id)
        {
            try
            {
                var result = _productDetailServices.FindDetailById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
