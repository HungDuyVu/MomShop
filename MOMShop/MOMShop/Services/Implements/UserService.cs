using AutoMapper;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

//using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MOMShop.Dto.Users;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Services.Interfaces.Mail;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using MOMShop.Utils.Mail;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MOMShop.Services.Implements
{
    public class UserService : IUserServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISendMailService _mailService;
        public UserService(ApplicationDbContext dbContext, IMapper mapper, ISendMailService mailService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mailService = mailService;
        }

        public APIResponse ChangePassword(ChangePasswordUserDto input)
        {
            var reponse = new APIResponse();
            var user = _dbContext.Users.FirstOrDefault(e => e.Email == input.Email);
            if (user == null) 
            {
                reponse.Message = "Tài khoản không tồn tại";
                return reponse;
            }
            var oldPassword = SeedData.GetMD5Hash(input.Password);
            if(oldPassword != user.Password)
            {
                reponse.Message = "Mật khẩu cũ không chính xác";
                return reponse;
            }

            if(input.Password == input.NewPassword)
            {
                reponse.Message = "Mật khẩu mới không được giống mật khẩu trước đó";
                return reponse;
            }
            var newPassword = SeedData.GetMD5Hash(input.NewPassword);
            user.Password = newPassword;

            _dbContext.SaveChanges();
            reponse.IsSuccess = true;
            reponse.Message = "Thay đổi mật khẩu thành công";
            return reponse;

        }

        public UserDto FindById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(e => e.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        public APIResponse ForgotPassword(string email)
        {
            var reponse = new APIResponse();
            var user = _dbContext.Users.FirstOrDefault(c => c.Email == email);
            if(user == null)
            {
                reponse.Message = "Không có dữ liệu email";
                return reponse;
            }
            var password = SeedData.RandomPassword();
            user.Password = SeedData.GetMD5Hash(password);
            _dbContext.SaveChanges();
            MailContent content = new MailContent
            {
                To = email,
                Subject = $"[THAY ĐỔI MẬT KHẨU]",
                Body = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <div style='text-align: center;'>
        <img src='https://cdn.pixabay.com/photo/2018/05/17/19/20/key-3409454_960_720.png' alt='Key Image' style='width: 150px; height: auto; margin-bottom: 20px;' />
    </div>
    <h1 style='color: #ee4d2d;'>Đây là mật khẩu mới của bạn</h1>
    <p style='font-size: 16px;'>Vui lòng không cung cấp mật khẩu mới cho bất cứ ai theo bất cứ hình thức nào để đảm bảo quyền riêng tư của bạn.</p>
    <div style='background-color: #f9f9f9; padding: 20px; border-radius: 8px; border: 1px solid #dedede;'>
        <p style='font-size: 18px; font-weight: bold; text-align: center;'>{password}</p>
    </div>
    <p style='font-size: 14px; color: #999; text-align: center; margin-top: 20px;'>Nếu bạn không yêu cầu thay đổi mật khẩu, vui lòng liên hệ với chúng tôi ngay lập tức.</p>
</div>"

            };
            var response = _mailService.SendMail(content);
            reponse.IsSuccess = true;
            reponse.Message = "Mật khẩu mới đã được cập nhật. Vui lòng kiểm tra email của bạn";

            return reponse;

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
