using AutoMapper;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.Mail;

namespace MOMShop.Services.Implements
{
    public class ExportOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISendMailService _mail;

        public ExportOrderService(ApplicationDbContext dbContext, IMapper mapper, ISendMailService mail)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mail = mail;
        }
    }
}
