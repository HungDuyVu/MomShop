using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Cart;
using MOMShop.Dto.Order;
using MOMShop.Dto.Order.User;
using MOMShop.Entites;
using MOMShop.Services.Interfaces.PaymentService;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using MOMShop.Utils.Payment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOMShop.Controllers.User
{
    [Route("api/user/order")]
    [ApiController]
    public class UserOrderController : ControllerBase
    {
        private readonly IUserOrderService _services;
        private readonly IPaymentService _paymentServices;
        public UserOrderController(IUserOrderService services, IPaymentService paymentServices)
        {
            _services = services;
            _paymentServices = paymentServices;
        }

        [HttpPost("add")]
        public APIResponse Create(OrderDto input)
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



        [HttpGet("get")]
        public List<ViewOrderDto> GetAllByUser([FromQuery] FilterOrderDto input)
        {
            try
            {
                var result = _services.FindAll(input);
                return result;
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

        [HttpPut("receive-notify")]
        public void RecceiveNotify([FromBody] PaymentRequestModel input)
        {
            try
            {
                _paymentServices.ReceiveNotify(input);
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
    }
}
