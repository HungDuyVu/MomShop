using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Collection;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System;
using MOMShop.Dto.Discount;

namespace MOMShop.Controllers
{
    [Route("api/discount")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private IDiscountService _services;

        public DiscountController(IDiscountService services)
        {
            _services = services;
        }

        [HttpGet("get-all-by-status")]
        public List<DiscountDto> GetAll(int status)
        {
            try
            {
                var result = _services.GetAllByStatus(status);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("find-all")]
        public List<DiscountDto> GetAll(FilterDiscountDto input)
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

        [HttpPost("add")]
        public DiscountDto Create([FromBody] DiscountDto input)
        {
            try
            {
                var result = _services.Add(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update")]
        public DiscountDto Update([FromBody] DiscountDto input)
        {
            try
            {
                var result = _services.Update(input);
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

        [HttpGet("find/{id}")]
        public DiscountDto Find(int id)
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

        [HttpPut("check")]
        public string Check(string discountCode)
        {
            try
            {
                var result = _services.CheckDiscountCodeExist(discountCode);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("check-discount")]
        public int CheckDiscount([FromQuery] string discountCode)
        {
            try
            {
                var result = _services.CheckDiscountPercent(discountCode);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
