using AutoMapper;
using MOMShop.Dto.Customer;
using MOMShop.Dto.Users;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils.APIResponse;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class CustomerService : ICustomerServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<UserDto> GetAll(FilterCustomerDto input)
        {
            var query = _dbContext.Users.Where(e => !e.Deleted && e.UserType == 2 && (input.Status == null || e.Status == input.Status)
            && (input.BirthDate == null || e.BirthDay != null && e.BirthDay.Value.Date == input.BirthDate.Value.Date)
            && (input.Keyword == null || e.FullName.ToLower().Contains(input.Keyword.ToLower()) || e.Email.ToLower().Contains(input.Keyword.ToLower())));
            var result = _mapper.Map<List<UserDto>>(query.ToList());
            return result;
        }

        public void ChangeStatus(int id)
        {
            var query = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            if (query.Status == 1)
            {
                query.Status = 2;
            } else {
                query.Status = 1;
            }
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var result = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            result.Deleted = true;
            _dbContext.SaveChanges();
        }

        public APIResponse Find(int id)
        {
            var result = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            return new APIResponse(_mapper.Map<UserDto>(result), "Ok");
        }
    }
}
