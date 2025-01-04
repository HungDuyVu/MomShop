using System;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string OrderCode { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Rating { get; set; }
        public bool Deleted { get; set; }
    }
}
