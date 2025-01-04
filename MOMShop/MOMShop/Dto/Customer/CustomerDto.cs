using MOMShop.Utils;
using System.ComponentModel.DataAnnotations;
using System;

namespace MOMShop.Dto.Customer
{
    public class CustomerDto 
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Deleted { get; set; }
    }
}
