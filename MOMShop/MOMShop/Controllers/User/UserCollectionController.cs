using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Product;
using MOMShop.Services.Interfaces.PaymentService;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils;
using System.Collections.Generic;
using System;
using MOMShop.Utils.APIResponse;

namespace MOMShop.Controllers.User
{
    [Route("api/user/collection")]
    [ApiController]
    public class UserCollectionController : ControllerBase
    {
        private readonly IUserCollectionService _services;
        public UserCollectionController(IUserCollectionService services)
        {
            _services = services;
        }

        [HttpGet]
        public APIResponse GetUserCollection()
        {
            try
            {
                var result = _services.GetUserCollection();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("detail/{id}")]
        public APIResponse GetDetailCollection(int id)
        {
            try
            {
                var result = _services.GetDetailCollection(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
