using System;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string BankAccount { get; set; }
        public string BankName { get; set; }
        public bool Deleted { get; set; }

    }
}
