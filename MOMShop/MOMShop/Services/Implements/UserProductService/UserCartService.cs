using AutoMapper;
using MOMShop.Dto.Cart;
using MOMShop.Entites;
using MOMShop.MomShopDbContext;
using MOMShop.Services.Interfaces.UserService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOMShop.Services.Implements.UserProductService
{
    public class UserCartService : IUserCartService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserCartService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public string Create(CartDto input)
        {
            var productCart = _dbContext.Carts.FirstOrDefault(e => e.ProductId == input.ProductId && e.Size == input.Size && e.CustomerId == input.CustomerId);
            if (productCart == null)
            {
                var insert = _mapper.Map<Cart>(input);
                insert.Quantity = 1;
                var entity = _dbContext.Carts.Add(insert);
                _dbContext.SaveChanges();
            } else
            {
                return "error";
            }
            return "success";
        }

        public void Delete(int id)
        {
            var cartItem = _dbContext.Carts.FirstOrDefault(e => e.Id == id);
            if (cartItem != null)
            {
                _dbContext.Carts.Remove(cartItem);
                _dbContext.SaveChanges();
            }
        }

        public List<ViewCartDto> GetAll(int? customerId)
        {
            var carts = _dbContext.Carts.Where(e => (customerId == null || e.CustomerId == customerId)).ToList();
            var result = new List<ViewCartDto>();
            foreach (var item in carts)
            {
                var resultItem = new ViewCartDto();
                var product = _dbContext.Products.FirstOrDefault(e => e.Id == item.ProductId && !e.Deleted);
                if (product != null)
                {
                    resultItem.Id = item.Id;
                    resultItem.ProductId = item.ProductId;
                    resultItem.ProductName = product.Name;
                    resultItem.Status = product.Status;
                    resultItem.Price = product.Price;
                    resultItem.ProductType = product.ProductType;
                    resultItem.Size = item.Size;
                    resultItem.Quantity = item.Quantity;

                    var productImage = _dbContext.ProductImages.FirstOrDefault(e => e.ProductId == item.ProductId);
                    if (productImage != null)
                    {
                        resultItem.ImageUrl = productImage.ImageUrl;
                    }
                }
                result.Add(resultItem);
            }
            return result;
        }

        public CartDto Update(CartDto input)
        {
            var cartItem = _dbContext.Carts.FirstOrDefault(e => e.Id == input.Id);
            if (cartItem != null)
            {
                _dbContext.SaveChanges();
            }
            return _mapper.Map<CartDto>(cartItem);
        }

        public void Update(UpdateCartDto input)
        {
            var cartDetail = _dbContext.Carts.FirstOrDefault(e => e.Id == input.Id);
            if (cartDetail == null)
            {
                throw new Exception("Không tìm thấy thông tin sản phẩm trong giỏ hàng");
            }
            cartDetail.Quantity = input.Quantity;
            _dbContext.SaveChanges();
        }
    }
}
