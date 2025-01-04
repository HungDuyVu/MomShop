using System;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public int UserType { get; set; }
        public string CreatedBy { get; set; }
        public int Status { get; set; }
        public bool Deleted { get; set; }
    }
}
