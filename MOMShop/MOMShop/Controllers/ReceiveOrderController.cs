using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using MOMShop.Dto.ReceiveOrder;
using MOMShop.Dto.ReceiveOrderDetail;
using MOMShop.Services.Implements;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using System;
using System.Collections.Generic;

namespace MOMShop.Controllers
{
    [Route("api/receive-order")]
    [ApiController]
    public class ReceiveOrderController : ControllerBase
    {
        private IReceiveOrderServices _services;
        private IReceiveOrderDetailService _detailServices;
        public ReceiveOrderController(IReceiveOrderServices services, IReceiveOrderDetailService detailServices)
        {
            _services = services;
            _detailServices = detailServices;
        }

        [HttpGet("find-all")]
        public List<ReceiveOrderDto> FindAll([FromQuery] FilterReceiveOrderDto input)
        {
            try
            {
                var result = _services.GetReceiveOrders(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("find-by-id/{id}")]
        public ReceiveOrderDto FindById(int id)
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

        [HttpPost("add")]
        public ReceiveOrderDto Add([FromBody] CreateReceiveOrderDto input)
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
        public ReceiveOrderDto Update([FromBody] CreateReceiveOrderDto input)
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

        [HttpGet("details/{id}")]
        public List<ReceiveOrderDetailDto> GetAll(int id)
        {
            try
            {
                var result = _detailServices.GetAll(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("add-detail")]
        public ReceiveOrderDetailDto AddDetail([FromBody] ReceiveOrderDetailDto input)
        {
            try
            {
                var result = _detailServices.Add(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("update-detail")]
        public ReceiveOrderDetailDto UpdateDetail([FromBody] ReceiveOrderDetailDto input)
        {
            try
            {
                var result = _detailServices.Update(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("delete-detail/{id}")]
        public void DeleteDetail(int id)
        {
            try
            {
                _detailServices.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("detail/find/{id}")]
        public ReceiveOrderDetailDto FinDetail(int id)
        {
            try
            {
                var result = _detailServices.FindById(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("payment/{id}")]
        public APIResponse Payment(int id)
        {
            try
            {
                _services.UpdateStatus(id, ReceiveOrderStatus.DA_THANH_TOAN);
                return new APIResponse("Ok");
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("completed/{id}")]
        public APIResponse Completed(int id)
        {
            try
            {
                _services.UpdateStatus(id, ReceiveOrderStatus.HOAN_THANH);
                return new APIResponse("Ok");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
