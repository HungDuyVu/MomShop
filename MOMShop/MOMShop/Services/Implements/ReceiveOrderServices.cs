using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOMShop.Dto.ReceiveOrder;
using MOMShop.Dto.ReceiveOrderDetail;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace MOMShop.Services.Implements
{
    public class ReceiveOrderServices : IReceiveOrderServices
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReceiveOrderServices(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public ReceiveOrderDto Add(CreateReceiveOrderDto input)
        {
            var insert = _mapper.Map<ReceiveOrder>(input);
            float total = 0;
            foreach (var item in input.Details)
            {
                // Kiểm tra sản phẩm có tồn tại trong bảng Product không
                var product = _dbContext.Products.FirstOrDefault(p => p.Code == item.Code && !p.Deleted);
                if (product == null)
                {
                    // Nếu sản phẩm không tồn tại, thêm mới sản phẩm
                    product = new Product
                    {
                        Code = item.Code,
                        Name = item.Name,
                        ProductType = item.Type,
                        Price = item.UnitPrice,
                        Description = "New product added automatically", // Mô tả mặc định
                        Status = 2, // Trạng thái sản phẩm mới là 2
                        Deleted = false
                    };
                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges(); // Lưu để lấy Id của sản phẩm
                }

                // Kiểm tra chi tiết sản phẩm trong bảng ProductDetail
                var productDetail = _dbContext.ProductDetails.FirstOrDefault(pd => pd.ProductId == product.Id && pd.Size == item.Size);

                if (productDetail != null)
                {
                    // Nếu ProductDetail tồn tại, cộng thêm số lượng
                    productDetail.Quantity += item.Quantity;
                    productDetail.Description = item.Description; // Cập nhật mô tả nếu cần
                }
                else
                {
                    // Nếu ProductDetail không tồn tại, tạo mới
                    _dbContext.ProductDetails.Add(new ProductDetail
                    {
                        ProductId = product.Id,
                        Size = item.Size,
                        Quantity = item.Quantity,
                        Description = item.Description
                    });
                }

                total += item.Quantity * item.UnitPrice;

            }
            var orderCode = RandomNumberGenerator.GenerateRandomNumber(8);

            while (true)
            {
                var orderCoedeFind = _dbContext.ReceiveOrders.FirstOrDefault(e => e.Code == orderCode && !e.Deleted);
                if (orderCoedeFind != null)
                {
                    orderCode = RandomNumberGenerator.GenerateRandomNumber(8);
                    break;
                }
                else
                {
                    break;
                }
            }

            insert.Code = orderCode;
            insert.TotalMoney = total;
            var result = _dbContext.Add(insert);
            _dbContext.SaveChanges();

            foreach (var item in input.Details)
            {
                var detail = new ReceiveOrderDetail()
                {
                    ReceiveOrderId = result.Entity.Id,
                    Code = item.Code,
                    Name = item.Name,
                    Type = item.Type,
                    Size = item.Size,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Description = item.Description,
                };
                _dbContext.ReceiveOrderDetails.Add(detail);
            }
            _dbContext.SaveChanges();

            return _mapper.Map<ReceiveOrderDto>(result.Entity);
        }

        public void Delete(int id)
        {
            var receiveOrder = _dbContext.ReceiveOrders.FirstOrDefault(e => e.Id == id);
            if (receiveOrder == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            var orderDetail = _dbContext.ReceiveOrderDetails.Where(e => e.ReceiveOrderId == id);
            _dbContext.ReceiveOrderDetails.RemoveRange(orderDetail);
            receiveOrder.Deleted = true;
            _dbContext.SaveChanges();
        }

        public ReceiveOrderDto FindById(int id)
        {
            var receiveOrder = _dbContext.ReceiveOrders.FirstOrDefault(e => e.Id == id);
            if (receiveOrder == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }

            var details = _dbContext.ReceiveOrderDetails.Where(e => e.ReceiveOrderId == id);
            var result = _mapper.Map<ReceiveOrderDto>(receiveOrder);

            if (details.Any())
            {
                result.Details = _mapper.Map<List<ReceiveOrderDetailDto>>(details);
            }
            return result;
        }

        public List<ReceiveOrderDto> GetReceiveOrders(FilterReceiveOrderDto input)
        {
            var resultItem = new List<ReceiveOrderDto>();
            var receiveOrders = _dbContext.ReceiveOrders.Where(e => !e.Deleted && (input.Status == null || e.Status == input.Status) && (input.Keyword == null
            || e.Code.Contains(input.Keyword)
            || e.Supplier.Contains(input.Keyword)
            || e.Receiver.Contains(input.Keyword)
            )
            && (input.CreatedDate == null || e.CreatedDate.Date == input.CreatedDate.Value.Date)
            && (input.IntendedTime == null || e.ReceivedDate.Date == input.IntendedTime.Value.Date)).OrderByDescending(e => e.Id).ToList();
            resultItem = _mapper.Map<List<ReceiveOrderDto>>(receiveOrders);
            foreach (var item in resultItem)
            {
                var orderDetail = _dbContext.ReceiveOrderDetails.Where(e => e.ReceiveOrderId == item.Id).OrderByDescending(e => e.Id).ToList();
                item.Details = _mapper.Map<List<ReceiveOrderDetailDto>>(orderDetail);
            }
            return resultItem;
        }

        public ReceiveOrderDto Update(CreateReceiveOrderDto input)
        {
            var receiveOrder = _dbContext.ReceiveOrders.FirstOrDefault(e => e.Id == input.Id);
            if (receiveOrder == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            receiveOrder.Code = input.Code;
            receiveOrder.ReceivedDate = input.ReceivedDate;
            receiveOrder.Supplier = input.Supplier;
            receiveOrder.Receiver = input.Receiver;
            receiveOrder.Description = input.Description;

            var products = _dbContext.ReceiveOrderDetails.Where(e => e.ReceiveOrderId == receiveOrder.Id && (!input.Details.Select(e => e.Code).Contains(e.Code) && !input.Details.Select(e => e.Size).Contains(e.Size))).ToList();
            foreach (var item in products)
            {
                _dbContext.ReceiveOrderDetails.Remove(item);
            }
            foreach (var item in input.Details)
            {
                var productCollection = _dbContext.ReceiveOrderDetails.FirstOrDefault(e => e.ReceiveOrderId == input.Id && e.Code == item.Code && e.Size == item.Size);
                if (productCollection == null)
                {
                    _dbContext.ReceiveOrderDetails.Add(new ReceiveOrderDetail
                    {
                        ReceiveOrderId = receiveOrder.Id,
                        Code = item.Code,
                        Size = item.Size,
                        Name = item.Name,
                        Quantity = item.Quantity,
                        Type = item.Type,
                        Description = item.Description
                    });
                }
                else
                {
                    productCollection.Code = item.Code;
                    productCollection.Size = item.Size;
                    productCollection.Name = item.Name;
                    productCollection.Quantity = item.Quantity;
                    productCollection.Type = item.Type;
                    productCollection.Description = item.Description;
                }
            }
            _dbContext.SaveChanges();
            return _mapper.Map<ReceiveOrderDto>(receiveOrder);
        }

        public void UpdateStatus(int id, int status)
        {
            var receiveOrder = _dbContext.ReceiveOrders.FirstOrDefault(e => e.Id == id);
            if (receiveOrder == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }

            receiveOrder.Status = status;
            _dbContext.SaveChanges();
        }
    }
}
