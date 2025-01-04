using MOMShop.Services.Interfaces.UserService;
using MOMShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using System.Collections.Generic;
using System;
using MOMShop.Dto.Cart;

namespace MOMShop.Controllers.User
{

    [Route("api/user/cart")]
    [ApiController]
    public class UserCartController : ControllerBase
    {
        private IUserCartService _services;
        public UserCartController(IUserCartService services)
        {
            _services = services;
        }

        [HttpGet("get-all")]
        public List<ViewCartDto> GetAll(int? customerId)
        {
            try
            {
                var result = _services.GetAll(customerId);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add")]
        public string Create(CartDto input)
        {
            try
            {
                var result = _services.Create(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public void Delete(int id)
        {
            try
            {
                _services.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update")]
        public void Update(UpdateCartDto input)
        {
            try
            {
                _services.Update(input);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
