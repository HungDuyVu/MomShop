    using System.ComponentModel;

    namespace MOMShop.Dto.Users
    {
        public class ChangePasswordUserDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }
    }
