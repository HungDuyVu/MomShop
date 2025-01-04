using MOMShop.Dto.Feedback;
using System.Collections.Generic;

namespace MOMShop.Services.Interfaces
{
    public interface IFeedbackServices
    {
        List<FeedbackDto> FindAll(FilterFeedbackDto input);
    }
}
