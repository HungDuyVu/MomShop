using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using MOMShop.Dto.Order;
using MOMShop.Dto.Order.User;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.UserService;
using MOMShop.Utils.HistoryUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using MOMShop.Dto.Users;
using System.Reflection.Metadata;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using MOMShop.Services.Interfaces.Mail;
using MOMShop.Utils.Mail;
using System.Threading.Tasks;
using MOMShop.Utils;
using Microsoft.AspNetCore.Mvc;
using MOMShop.Utils.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MOMShop.Utils.APIResponse;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using MOMShop.Dto;
using Org.BouncyCastle.Utilities.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace MOMShop.Services.Implements.UserProductService
{
    public class UserOrderService : IUserOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISendMailService _mail;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<VNPaySettings> _vnPaySettings;

        public UserOrderService(ApplicationDbContext dbContext, IMapper mapper, ISendMailService mail, IHttpContextAccessor httpContextAccessor, IOptions<VNPaySettings> vnPaySettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mail = mail;
            _httpContextAccessor = httpContextAccessor;
            _vnPaySettings = vnPaySettings;
        }

        public APIResponse Create(OrderDto order)
        {

            order.CreatedDate = DateTime.Now;
            order.IntendedTime = DateTime.Now.AddDays(3);
            var insert = _mapper.Map<Order>(order);
            var orderCode = RandomNumberGenerator.GenerateRandomNumber(8);

            while (true)
            {
                var orderCoedeFind = _dbContext.Orders.FirstOrDefault(e => e.OrderCode == orderCode && !e.Deleted);
                if (orderCoedeFind != null)
                {
                    orderCode = RandomNumberGenerator.GenerateRandomNumber(8);
                    break;
                } else
                {
                    break;
                }
            }

            insert.OrderCode = orderCode;
            var transaction = _dbContext.Database.BeginTransaction();
            Order orderGet = new Order();
            try
            {
                var result = _dbContext.Orders.Add(insert);
                orderGet = result.Entity;
                _dbContext.SaveChanges();

                foreach (var item in order.OrderDetails)
                {
                    var detail = _mapper.Map<OrderDetail>(item);
                    var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId && !e.Deleted);
                    var productDetail = _dbContext.ProductDetails.FirstOrDefault(p => p.ProductId == item.ProductId && p.Size == item.Size);
                    if (productDetail != null)
                    {
                        if (productDetail.Quantity <= 0)
                        {
                            return new APIResponse("hethang");
                           
                        }
                        productDetail.Quantity = productDetail.Quantity - item.Quantity;
                        var productDetails = _dbContext.ProductDetails.Where(p => p.ProductId == item.ProductId);
                        if (productDetail.Quantity <= 0)
                        {
                            product.Status = Status.HET_HANG;
                        }
                        if(productDetails.All(e => e.Quantity == 0))
                        {
                            product.Status = Status.HET_HANG;
                        }
                    }
                    detail.OrderId = result.Entity.Id;
                    _dbContext.OrderDetails.Add(detail);
                }
                _dbContext.SaveChanges();

                foreach (var item in order.OrderDetails)
                {
                    var cart = _dbContext.Carts.FirstOrDefault(e => e.ProductId == item.ProductId && e.CustomerId == order.CreatedBy);
                    if (cart != null)
                    {
                        _dbContext.Carts.Remove(cart);
                    }
                }

                var history = new HistoryUpdate()
                {
                    Table = HistoryUpdateTable.ORDER,
                    ReferId = result.Entity.Id,
                    Summary = "Thêm mới đơn hàng"
                };
                _dbContext.HistoryUpdates.Add(history);
                _dbContext.SaveChanges();
                transaction.Commit();
                
            }
            catch (Exception ex)
            {
                transaction.Rollback();
            }

           
            //Cấu hình thông tin SMTP
            //try
            //{
            //    //Lấy dịch vụ sendmailservice
            //    MailContent content = new MailContent
            //    {
            //        To = "giangcoi2001@gmail.com",
            //        Subject = $"[ĐƠN HÀNG {orderCode} ĐÃ ĐƯỢC ĐẶT THÀNH CÔNG]",
            //        Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{orderCode}</h2>\r\n    <p>Cảm ơn bạn đã đặt hàng, đơn hàng sẽ sớm được xử lý.</p>\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n        <a href=\"http://localhost:4200/order\">Xem đơn hàng</a>\r\n        hoặc\r\n        <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
            //    };
            //    _mail.SendMail(content);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Failed to send email: " + ex.Message);
            //}

            if (order.PaymentType == PaymentTypes.CHUYEN_KHOAN)
            {
                //Get payment input
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.OrderId = (long)orderGet.Id; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                orderInfo.Amount = (long)orderGet.TotalAmount; // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                orderInfo.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
                orderInfo.CreatedDate = DateTime.Now;
                //Save order to db

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", _vnPaySettings.Value.Vnp_Command);
                vnpay.AddRequestData("vnp_TmnCode", _vnPaySettings.Value.Vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (orderInfo.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
                vnpay.AddRequestData("vnp_CreateDate", orderInfo.CreatedDate.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", _vnPaySettings.Value.Vnp_CurrCode);
                vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
                vnpay.AddRequestData("vnp_Locale", _vnPaySettings.Value.Vnp_Locale);
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + orderInfo.OrderId);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                vnpay.AddRequestData("vnp_ReturnUrl", "http://localhost:4200/response");
                vnpay.AddRequestData("vnp_TxnRef", orderInfo.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                string paymentUrl = vnpay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", _vnPaySettings.Value.Vnp_SecureHash);
                return new APIResponse(insert, paymentUrl);
            }

            return new APIResponse(insert, "done");
        }

        public List<ViewOrderDto> FindAll(FilterOrderDto input)
        {
            var orders = _dbContext.Orders.Where(e => e.CreatedBy == input.CustomerId && !e.Deleted && !e.UserDelete
                                                    && (input.Status == null || e.OrderStatus == input.Status) 
                                                    && (input.OrderCode == null || e.OrderCode.Contains(input.OrderCode))).OrderByDescending(e => e.Id).ToList();
            var result = _mapper.Map<List<ViewOrderDto>>(orders);
            foreach (var item in result)
            {
                var orderDetail = _dbContext.OrderDetails.Where(e => e.OrderId == item.Id).ToList();
                item.Details = _mapper.Map<List<ViewOrderDetail>>(orderDetail);
                foreach (var detail in item.Details)
                {
                    var product = _dbContext.Products.FirstOrDefault(e => e.Id == detail.ProductId && !e.Deleted);
                    if (product != null)
                    {
                        detail.Name = product.Name;
                        detail.Price = product.Price;
                        var productImage = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == product.Id);
                        detail.ImageUrl = productImage?.ImageUrl;
                        detail.Total = detail.Price * detail.Quantity;
                    }
                }
            }

            if (input.Keyword != null)
            {
                result = result.Where(e => e.Details.Where(detail => detail.Name.Contains(input.Keyword)).Any()).ToList();
            }

            return result;
        }

        public OrderDto FindById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(int id, int status)
        {
            string summary = null;

            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (order != null)
            {
                order.OrderStatus = status;
            }
            if (status == OrderStatus.DA_HUY && order.OrderStatus == OrderStatus.DA_HUY)
            {
                summary = "Hủy đơn hàng";
                // Lấy dịch vụ sendmailservice
                MailContent content = new MailContent
                {
                    To = order.Email,
                    Subject = $"[ĐƠN HÀNG {order.OrderCode} ĐÃ ĐƯỢC HỦY]",
                    Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{order.OrderCode}</h2>\r\n    <p>Đơn hàng đã được hủy. Nếu bạn quan tâm các sản phẩm khác xin vui lòng truy cập trang web của sshop để biết thêm thông tin chi tiết.</p>" +
                    $"\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>" +
                    $"\r\n  <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
                };
                _mail.SendMail(content);
            }
            var history = new HistoryUpdate()
            {
                Table = HistoryUpdateTable.ORDER,
                ReferId = order.Id,
                Summary = summary
            };
            _dbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (order != null)
            {
                order.UserDelete = true;
            }
            _dbContext.SaveChanges();
        }
    }
}
