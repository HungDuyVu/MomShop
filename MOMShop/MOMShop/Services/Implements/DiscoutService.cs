using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using MOMShop.Dto.Discount;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements
{
    public class DiscoutService : IDiscountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DiscoutService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public DiscountDto Add(DiscountDto input)
        {
            var insert = _mapper.Map<Discount>(input);       
            _dbContext.Discounts.Add(insert);
            _dbContext.SaveChanges();
            return input;
        }

        public string CheckDiscountCodeExist(string discountCode)
        {
            var check = _dbContext.Discounts.FirstOrDefault(d => d.DiscountCode == discountCode);
            if (check == null)
            {
                return "success";
            }
            return "duplicate";
        }

        public int CheckDiscountPercent(string discountCode)
        {
            var check = _dbContext.Discounts.FirstOrDefault(d => d.DiscountCode == discountCode);
            if (check != null)
            {
                return check.DiscountPercent;
            }
            return 0;
        }

        public void Delete(int id)
        {
            var check = _dbContext.Discounts.FirstOrDefault(d => d.Id == id);
            if (check != null)
            {
                _dbContext.Discounts.Remove(check);
            }
            _dbContext.SaveChanges();
        }

        public DiscountDto Find(int id)
        {
            var check = _dbContext.Discounts.FirstOrDefault(d => d.Id == id);
            if (check != null)
            {
                return _mapper.Map<DiscountDto>(check);
            }
            return null;
        }

        public List<DiscountDto> GetAll(FilterDiscountDto input)
        {
            var result = _dbContext.Discounts.Where(e => (input.DiscountCode == null || e.DiscountCode.Contains(input.DiscountCode)
                                                        && (input.Status == null || input.Status == e.Status))).ToList();

            return _mapper.Map<List<DiscountDto>>(result);
        }

        public List<DiscountDto> GetAllByStatus(int status)
        {
            var check = _dbContext.Discounts.Where(d => d.Status == status).ToList();
            if (check != null)
            {
                return _mapper.Map<List<DiscountDto>>(check);
            }
            return null;
        }

        public DiscountDto Update(DiscountDto input)
        {
            var check = _dbContext.Discounts.FirstOrDefault(d => d.Id == input.Id);
            if (check != null)
            {
                check.DiscountPercent = input.DiscountPercent;
                check.Status = input.Status;
            }
            return _mapper.Map<DiscountDto>(check);   
        }
    }
}
