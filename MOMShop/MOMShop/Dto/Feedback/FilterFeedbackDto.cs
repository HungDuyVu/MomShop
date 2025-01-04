using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils;

namespace MOMShop.Dto.Feedback
{
    public class FilterFeedbackDto : PagingBase
    {
        [FromQuery(Name = "rating")]
        public int? Rating { get; set; }
    }
}
