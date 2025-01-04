using AutoMapper;
using MOMShop.Dto.Feedback;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class FeedbackService : IFeedbackServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public FeedbackService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<FeedbackDto> FindAll(FilterFeedbackDto input)
        {
            var result = _dbContext.Feedbacks.Where(e => !e.Deleted 
            && (input.Keyword == null || e.OrderCode.ToLower().Contains(input.Keyword.ToLower()) || e.CustomerName.ToLower().Contains(input.Keyword.ToLower()))
            && (input.Rating == null || e.Rating == input.Rating)).ToList();
            return _mapper.Map<List<FeedbackDto>>(result);
        }
    }
}
