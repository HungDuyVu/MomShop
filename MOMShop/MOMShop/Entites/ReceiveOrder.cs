using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class ReceiveOrder
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReceivedDate { get; set; }
        [Comment("Nguoi gui (Xuong may gui))")]
        public string Supplier { get; set; }
        public string Receiver { get; set; }
        /// <summary>
        /// 1. Chưa thanh toán, 2 đã thanh toán, 3 Đã hoàn thành
        /// </summary>
        public int Status { get; set; }
        public string Description { get; set; }
        public float TotalMoney { get; set; }
        public bool Deleted { get; set; }

    }
}
