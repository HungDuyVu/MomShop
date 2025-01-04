using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOMShop.Dto.Order;
using MOMShop.Dto.Product;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Services.Interfaces.Mail;
using MOMShop.Utils;
using MOMShop.Utils.HistoryUpdate;
using MOMShop.Utils.Mail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class OrderServices : IOrderServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISendMailService _mail;

        public OrderServices(ApplicationDbContext dbContext, IMapper mapper, ISendMailService mail)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mail = mail;
        }

        public void Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
            }
            _dbContext.SaveChanges();
        }   

        public OrderDto FindById(int id)
        {

            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (order != null)
            {
                var result = _mapper.Map<OrderDto>(order);
                var orderDetail = _dbContext.OrderDetails.Where(e => e.OrderId == order.Id);
                result.OrderDetails = _mapper.Map<List<CreateOrderDetailDto>>(orderDetail);
                foreach (var item in result.OrderDetails)
                {
                    var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId);
                    if (product != null)
                    {
                        item.Price = product.Price;
                        item.ProductCode = product.Code;
                        item.ProductName = product.Name;
                        var imageUrl = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == item.ProductId);
                        item.ImageUrl = imageUrl?.ImageUrl;
                    }
                }

                var events = _dbContext.HistoryUpdates.Where(e => e.Table == HistoryUpdateTable.ORDER && e.ReferId == order.Id);
                result.Events = _mapper.Map<List<Events>>(events);

                return result;
            }
            return null;
        }

        public List<OrderDto> GetAllOrder(FilterOrderDto input)
        {
            var result = new List<OrderDto>();
            var orders = _dbContext.Orders.Where(e => !e.Deleted && (input.Status == null || e.OrderStatus == input.Status)
            && (input.Keyword == null || e.OrderCode.Contains(input.Keyword) || e.CustomerName.ToLower().Contains(input.Keyword))
            && (input.CreatedDate == null || e.CreatedDate.Date == input.CreatedDate.Value.Date)
            && (input.IntendedTime == null || e.IntendedTime.Date == input.IntendedTime.Value.Date)).OrderByDescending(e => e.Id).ToList();

            result = _mapper.Map<List<OrderDto>>(orders);
            return result;
        }

        public void UpdateStatus(int id, int status)
        {
            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if(order != null)
            {
                order.OrderStatus = status;
            }
            string summary = null;
            if (status == OrderStatus.DA_NHAN && order.OrderStatus == OrderStatus.DA_NHAN)
            {
                summary = "Đơn hàng đã được tiếp nhận";
                // Cấu hình thông tin SMTP
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = order.Email,
                        Subject = $"[ĐƠN HÀNG {order.OrderCode} ĐANG ĐƯỢC XỬ LÝ]",
                        Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{order.OrderCode}</h2>\r\n    <p>Đơn hàng đã được tiếp nhận và sẽ sớm được giao đến cho bạn.</p>\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n        <a href=\"http://localhost:4200/order\">Xem đơn hàng</a>\r\n        hoặc\r\n        <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
                    };
                    _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            } else if (status == OrderStatus.DA_GIAO && order.OrderStatus == OrderStatus.DA_GIAO)
            {
                summary = "Đơn hàng đang được vận chuyển";
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = order.Email,
                        Subject = $"[ĐƠN HÀNG {order.OrderCode} ĐANG ĐƯỢC VẬN CHUYỂN]",
                        Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{order.OrderCode}</h2>\r\n    <p>Đơn hàng đang được vận chuyển và sẽ sớm được giao đến cho bạn.</p>\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n        <a href=\"http://localhost:4200/order\">Xem đơn hàng</a>\r\n        hoặc\r\n        <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
                    };
                    _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            else if (status == OrderStatus.HOAN_THANH && order.OrderStatus == OrderStatus.HOAN_THANH)
            {
                // Lấy dịch vụ sendmailservice
                MailContent content = new MailContent
                {
                    To = order.Email,
                    Subject = $"[ĐƠN HÀNG {order.OrderCode} ĐÃ ĐƯỢC GIAO]",
                    Body = $"<h1>MOMSHOP</h1>\r\n    <h2>ĐƠN HÀNG #{order.OrderCode}</h2>\r\n    <p>Đơn hàng đã được giao đến cho bạn. Nếu bạn quan tâm các sản phẩm khác xin vui lòng truy cập trang web của sshop để biết thêm thông tin chi tiết.</p>" +
                    $"\r\n    <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>" +
                    $"\r\n  <a href=\"http://localhost:4200/view\">Đến cửa hàng của chúng tôi</a>\r\n    </p>"
                };
                _mail.SendMail(content);
            }
            else if (status == OrderStatus.DA_HUY && order.OrderStatus == OrderStatus.DA_HUY)
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

            _dbContext.HistoryUpdates.Add(history);
            _dbContext.SaveChanges();   
        }

        public ViewOrderReciptDto ViewOrder(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(e => e.Id == id && !e.Deleted);
            if (order != null)
            {
                var result = _mapper.Map<ViewOrderReciptDto>(order);
                var orderDetail = _dbContext.OrderDetails.Where(e => e.OrderId == order.Id);
                result.Address = $"{result.Address}, {result.District}, {result.Province}, {result.Nation}";
                result.OrderDetails = _mapper.Map<List<CreateOrderDetailDto>>(orderDetail);
                foreach (var item in result.OrderDetails)
                {
                    var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId);
                    if (product != null)
                    {
                        item.Price = product.Price;
                        item.ProductCode = product.Code;
                        item.ProductName = $"{product.Name} ({item.Size})";
                    }
                }
                return result;
            }
            return null;
        }
    }
}
