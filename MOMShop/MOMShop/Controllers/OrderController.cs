using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Order;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using System;
using System.Collections.Generic;

namespace MOMShop.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderServices _services;
        private IOrderDetailServices _orderServices;
        public OrderController(IOrderServices services, IOrderDetailServices orderServices)
        {
            _services = services;
            _orderServices = orderServices;
        }

        [HttpGet("find-all")]
        public List<OrderDto> GetOrders([FromQuery] FilterOrderDto input)
        {
            try
            {
                var result = _services.GetAllOrder(input);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("detail/{id}")]
        public OrderDto Get(int id)
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

        [HttpGet("view/{id}")]
        public ViewOrderReciptDto View(int id)
        {
            try
            {
                var result = _services.ViewOrder(id);
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

        [HttpPut("update-status")]
        public void UpdateStatus(int id, int status)
        {
            try
            {
                _services.UpdateStatus(id, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("accept-order/{id}")]
        public void AcceptOrder(int id)
        {
            try
            {
                _services.UpdateStatus(id, OrderStatus.DA_NHAN);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("process-order/{id}")]
        public void ProcessOrder(int id)
        {
            try
            {
                _services.UpdateStatus(id, OrderStatus.DA_GIAO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("complete-order/{id}")]
        public void CompleteOrder(int id)
        {
            try
            {
                _services.UpdateStatus(id, OrderStatus.HOAN_THANH);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("cancel-order/{id}")]
        public void CancelOrder(int id)
        {
            try
            {
                _services.UpdateStatus(id, OrderStatus.DA_HUY);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
