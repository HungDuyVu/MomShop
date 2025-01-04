using MOMShop.Dto.Feedback;

namespace MOMShop.Services.Interfaces.UserService
{
    public interface IUserFeedbackService
    {
        FeedbackDto Create(FeedbackDto input);
    }
}
