using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using MOMShop.Services.Interfaces;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils;
using System;
using System.Collections.Generic;

namespace MOMShop.Controllers.User
{
    [Route("api/user/product")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private IUserProductService _services;
        private IProductDetailServices _productDetailServices;
        public UserProductController(IUserProductService services, IProductDetailServices productDetailServices)
        {
            _services = services;
            _productDetailServices = productDetailServices;
        }

        [HttpGet("t-shirt")]
        public List<ProductDto> GetTShirts([FromQuery] FilterProductDto input)
        {
            try
            {
                input.ProductType = ProductType.AO_THUN;
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("shirt")]
        public List<ProductDto> GetShirts([FromQuery] FilterProductDto input)
        {
            try
            {
                input.ProductType = ProductType.AO_SO_MI;
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("jacket")]
        public List<ProductDto> GetJackets([FromQuery] FilterProductDto input)
        {
            try
            {
                input.ProductType = ProductType.AO_KHOAC;
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("short")]
        public List<ProductDto> GetShorts([FromQuery] FilterProductDto input)
        {
            try
            {
                input.ProductType = ProductType.QUAN;
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("accessory")]
        public List<ProductDto> GetAccessories([FromQuery] FilterProductDto input)
        {
            try
            {
                input.ProductType = ProductType.PHU_KIEN;
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("all")]
        public List<ProductDto> GetAll([FromQuery] FilterProductDto input)
        {
            try
            {
                var result = _services.GetAllProductForUser(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
