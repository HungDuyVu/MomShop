using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOMShop.Dto;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.Mail;
using MOMShop.Services.Interfaces.PaymentService;
using MOMShop.Utils;
using MOMShop.Utils.APIResponse;
using MOMShop.Utils.HistoryUpdate;
using MOMShop.Utils.Mail;
using MOMShop.Utils.Payment;
using Org.BouncyCastle.Asn1.X9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MOMShop.Services.Implements.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISendMailService _mail;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly VNPaySettings _vnPaySettings;
        private readonly HttpClient _httpClient;

        public PaymentService(IOptions<VNPaySettings> vnPaylSettings, ApplicationDbContext dbContext, IMapper mapper, ISendMailService mail, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mail = mail;
            _httpContextAccessor = httpContextAccessor;
            _vnPaySettings = vnPaylSettings.Value;
            _httpClient = new HttpClient();
        }

        public void CreatePayment1(PaymentRequestModel input)
        {
            
        }
        private string GenerateChecksum(string data, string key)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hmacBytes = hmac.ComputeHash(dataBytes);
                string hmacString = BitConverter.ToString(hmacBytes).Replace("-", "").ToLower();
                return hmacString;
            }
        }
        public APIResponse PaymentNotification(PaymentNotificationModel input)
        {
            throw new System.NotImplementedException();
        }

        public string CalculateHMACSHA512(Dictionary<string, string> data)
        {
            // Sắp xếp các trường thông tin theo thứ tự alphabet
            var sortedData = new SortedDictionary<string, string>(data);

            // Tạo chuỗi dữ liệu
            string dataString = "";
            foreach (var entry in sortedData)
            {
                dataString += entry.Key + "=" + entry.Value + "&";
            }
            dataString = dataString.TrimEnd('&');

            // Thêm secret key vào chuỗi dữ liệu
            var secretKey = _vnPaySettings.Vnp_SecureHash;
            dataString += "&" + secretKey;

            // Tính giá trị HMACSHA512 cho chuỗi dữ liệu
            byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(dataString);

            using (HMACSHA512 hmac = new HMACSHA512(secretKeyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                return hashString;
            }
        }

        public void ReceiveNotify(PaymentRequestModel input)
        {
            if (input.StatusCode == "00")
            {
                var order = _dbContext.Orders.FirstOrDefault(e => e.Id.ToString() == input.OrderId && !e.Deleted);
                if (order != null)
                {
                    order.OrderStatus = OrderStatus.DA_THANH_TOAN;
                    var history = new HistoryUpdate()
                    {
                        Table = HistoryUpdateTable.ORDER,
                        ReferId = order.Id,
                        Summary = "Thanh toán đơn hàng",
                    };
                    _dbContext.HistoryUpdates.Add(history);
                    //Cấu hình thông tin SMTP
                    try
                    {
                        //Lấy dịch vụ sendmailservice
                        MailContent content = new MailContent
                        {
                            To = "giangcoi2001@gmail.com",
                            Subject = $"[ĐƠN HÀNG {order.OrderCode} ĐÃ ĐƯỢC ĐẶT THÀNH CÔNG]",
                            Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{order.OrderCode}</h2>\r\n    <p>Đơn hàng đã được thanh toán. Cảm ơn bạn đã đặt hàng, đơn hàng sẽ sớm được xử lý.</p>\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n        <a href=\"http://localhost:4200/order\">Xem đơn hàng</a>\r\n        hoặc\r\n        <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
                        };
                        _mail.SendMail(content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
