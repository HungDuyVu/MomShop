using System;

namespace MOMShop.Dto.Users
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
    }
}
