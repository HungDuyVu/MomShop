using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOMShop.Entites
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Nation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// Ngày đặt
        /// </summary>
        [Comment("Ngay tao don")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Thời gian nhận hàng dự kiến
        /// </summary>
        [Comment("Thoi gian nhan hang du kien")]
        public DateTime IntendedTime { get; set; }
        [Comment("1. Cod, 2.Bank")]
        public int PaymentType { get; set; }
        [Comment("1. Khoi tao, 2.Dang giao va chua nhan tien , 3.Dang giao va da nhan tien, 4. Hoan thanh, 5. Da xoa")]
        public int OrderStatus { get; set; }
        public bool UserDelete { get; set; }
        public float TotalAmount { get; set; }
        public float DeliveryCost { get; set; }
        public string DiscountCode { get; set; }
        public int? CreatedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
