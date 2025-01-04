using AutoMapper;
using MOMShop.Dto.Feedback;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.UserService;

namespace MOMShop.Services.Implements.UserProductService
{
    public class UserFeedBackService : IUserFeedbackService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserFeedBackService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public FeedbackDto Create(FeedbackDto input)
        {
            var insert = _mapper.Map<Feedback>(input);
            _dbContext.Feedbacks.Add(insert);
            _dbContext.SaveChanges();
            return input;
        }
    }
}
