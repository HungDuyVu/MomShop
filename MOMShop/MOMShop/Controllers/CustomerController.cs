using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Collection;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System;
using MOMShop.Dto.Customer;
using MOMShop.Dto.Users;
using MOMShop.Utils.APIResponse;

namespace MOMShop.Controllers
{
    [Route("api/user-account")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerServices _services;

        public CustomerController(ICustomerServices services)
        {
            _services = services;
        }

        [HttpGet("get-all")]
        public List<UserDto> GetAll([FromQuery] FilterCustomerDto input)
        {
            try
            {
                var result = _services.GetAll(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public APIResponse Get(int id)
        {
            try
            {
                var result = _services.Find(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("change-status/{id}")]
        public APIResponse ChangeStatus(int id)
        {
            try
            {
                 _services.ChangeStatus(id);
                return new APIResponse("ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public APIResponse Delete(int id)
        {
            try
            {
                _services.Delete(id);
                return new APIResponse("ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
