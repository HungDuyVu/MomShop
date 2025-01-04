using System;

namespace MOMShop.Dto.Feedback
{
    public class FeedbackDto
    {
        public int? Id { get; set; }
        public string CustomerName { get; set; }
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Rating { get; set; }
        public bool Deleted { get; set; }
    }
}
