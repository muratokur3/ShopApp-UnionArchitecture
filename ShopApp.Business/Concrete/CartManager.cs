using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public void AddToCart(string userId, int productId, int quantity)
        //{
        //    var cart = GetCartByUserId(userId);
        //    if (cart != null)
        //    {
        //        var index = cart.CartItems.FindIndex(i => i.ProductId == productId);
        //        if (index < 0)
        //        {
        //            cart.CartItems.Add(new CartItem()
        //            {
        //                ProductId = productId,
        //                Quantity = quantity,
        //                CartId = cart.Id
        //            });
        //        }
        //        else
        //        {
        //            cart.CartItems[index].Quantity += quantity;
        //        }
        //        _unitofwork.Carts.Update(cart);
        //        _unitofwork.save();
        //    }

        //}

        //public void ClearCart(int cartId)
        //{
        //    _unitofwork.Carts.ClearCart(cartId);
        //}

        //public void DeleteFromCart(string userId, int productId)
        //{
        //   var cart = GetCartByUserId(userId);
        //    if (cart != null)
        //    {

        //        _unitofwork.Carts.DeleteFromCart(cart.Id,productId);
        //    }
        //}

        //public Cart GetCartByUserId(string userId)
        //{
        //   return _unitofwork.Carts.GetByUserId(userId);
        //}

        //public void initializeCart(string userId)
        //{
        //    _unitOfWork.Carts.Create(new Cart() { UserId = userId });
        //    _unitofwork.save();
        //}
    }
}
