using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MOMShop.Dto.Users;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MOMShop.Services.Implements
{
    public class UserService : IUserServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDto FindById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto Login(LoginDto input)
        {
            var admin = SeedData.Admin();
            var password = SeedData.GetMD5Hash(input.Password);

            if (input.Email == admin.Email && password == admin.Password)
            {
                return _mapper.Map<UserDto>(admin);
            }
            var user = _dbContext.Users.FirstOrDefault(e => e.Email == input.Email && e.Password == password);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<UserDto>(user);
        }

        public APIResponse Register(RegisterDto input)
        {
            var insert = _mapper.Map<Users>(input);

            var user = _dbContext.Users.FirstOrDefault(e => e.Email == input.Email && !e.Deleted);
            if (user != null)
            {
                return new APIResponse("duplicate");
            }
            var password = SeedData.GetMD5Hash(input.Password);
            insert.Password = password;
            var result = _dbContext.Users.Add(insert);
            _dbContext.SaveChanges();
            return new APIResponse(result.Entity, "ok");
        }
        public string UpdateInforUser(UserDto input)
        {
            var userUpdate = _dbContext.Users.FirstOrDefault(e => e.Id == input.Id);
            userUpdate.FullName = input.FullName;
            userUpdate.BirthDay = input.BirthDay;
            userUpdate.Gender = input.Gender;
            userUpdate.Address = input.Address;
            userUpdate.Province = input.Province;
            userUpdate.District = input.District;
            userUpdate.Phone = input.Phone;
            _dbContext.SaveChanges();
            return "success";
        }

        
    }
}
