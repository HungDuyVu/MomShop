using System.Collections.Generic;
using MOMShop.Dto.Discount;

namespace MOMShop.Services.Interfaces
{
    public interface IDiscountService
    {
        DiscountDto Add(DiscountDto input);
        DiscountDto Update(DiscountDto input);
        void Delete (int id);
        List<DiscountDto> GetAll(FilterDiscountDto input);
        List<DiscountDto> GetAllByStatus(int status);
        DiscountDto Find(int id);
        string CheckDiscountCodeExist(string discountCode);
        int CheckDiscountPercent(string discountCode);
    }
}
