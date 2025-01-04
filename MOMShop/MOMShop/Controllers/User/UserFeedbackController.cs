using Microsoft.AspNetCore.Mvc;
using MOMShop.Dto.Feedback;
using MOMShop.Dto.Order;
using MOMShop.Entites;
using MOMShop.Services.Interfaces.UserService;
using System;

namespace MOMShop.Controllers.User
{
    [Route("api/user/feedback")]
    [ApiController]
    public class UserFeedbackController : ControllerBase
    {
        private IUserFeedbackService _services;
        public UserFeedbackController(IUserFeedbackService services)
        {
            _services = services;
        }

        [HttpPost("add")]
        public FeedbackDto Create(FeedbackDto input)
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
    }
}
