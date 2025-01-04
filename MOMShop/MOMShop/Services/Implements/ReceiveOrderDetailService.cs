
using AutoMapper;
using MOMShop.Dto.ReceiveOrder;
using MOMShop.Dto.ReceiveOrderDetail;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using MOMShop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class ReceiveOrderDetailService : IReceiveOrderDetailService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReceiveOrderDetailService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public ReceiveOrderDetailDto Add(ReceiveOrderDetailDto input)
        {
            var insert = _mapper.Map<ReceiveOrderDetail>(input);
            _dbContext.Add(insert);
            _dbContext.SaveChanges();
            return input;
        }

        public void Delete(int id)
        {
            var receiveOrderDetail = _dbContext.ReceiveOrderDetails.FirstOrDefault(e => e.Id == id);
            if (receiveOrderDetail == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            _dbContext.Remove(receiveOrderDetail);
            _dbContext.SaveChanges();
        }

        public ReceiveOrderDetailDto FindById(int id)
        {
            var receiveOrderDetail = _dbContext.ReceiveOrderDetails.FirstOrDefault(e => e.Id == id);
            if (receiveOrderDetail == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            return _mapper.Map<ReceiveOrderDetailDto>(receiveOrderDetail);
        }

        public List<ReceiveOrderDetailDto> GetAll(int receiveOrderId)
        {
            var resultItem = new List<ReceiveOrderDetailDto>();
            var receiveOrderDetails = _dbContext.ReceiveOrderDetails.Where(e => e.ReceiveOrderId == receiveOrderId ).OrderByDescending(e => e.Id).ToList();

            return _mapper.Map<List<ReceiveOrderDetailDto>>(receiveOrderDetails);
        }

        public ReceiveOrderDetailDto Update(ReceiveOrderDetailDto input)
        {
            var receiveOrderDetail = _dbContext.ReceiveOrderDetails.FirstOrDefault(e => e.Id == input.Id);
            if (receiveOrderDetail == null)
            {
                throw new Exception("Không tìm thấy sản phẩm");
            }
            receiveOrderDetail.Code = input.Code;
            receiveOrderDetail.Name = input.Name;
            receiveOrderDetail.Quantity = input.Quantity;
            receiveOrderDetail.Type = input.Type;
            receiveOrderDetail.Size = input.Size;
            receiveOrderDetail.UnitPrice = input.UnitPrice;
            receiveOrderDetail.Description = input.Description;
            _dbContext.SaveChanges();
            return _mapper.Map<ReceiveOrderDetailDto>(receiveOrderDetail);
        }
    }
}
