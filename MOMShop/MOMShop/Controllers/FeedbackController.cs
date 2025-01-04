using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Customer;
using MOMShop.Dto.Users;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System;
using MOMShop.Dto.Feedback;

namespace MOMShop.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private IFeedbackServices _services;

        public FeedbackController(IFeedbackServices services)
        {
            _services = services;
        }

        [HttpGet("get-all")]
        public List<FeedbackDto> FindAll([FromQuery] FilterFeedbackDto input)
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
    }
}
