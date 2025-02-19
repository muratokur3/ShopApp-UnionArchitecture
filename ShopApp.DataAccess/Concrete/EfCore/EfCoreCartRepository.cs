﻿using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartRepository : EfcoreGenericRepository<Cart>, ICartRepository
    {
        public EfCoreCartRepository(ShopContext context) : base(context)
        {
        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        public void ClearCart(int cartId)
        {
                var cmd = @"delete from CartItems where CartId=@p0";
                ShopContext.Database.ExecuteSqlRaw(cmd, cartId.ToString());
        }

        public void DeleteFromCart(int carId, int productId)
        {
                var cmd = @"delete from CartItems where CartId=@p0 And ProductId=@p1";
                ShopContext.Database.ExecuteSqlRaw(cmd, carId, productId);
        }

        //public Cart GetByUserId(string userId)
        //{
        //        return ShopContext.Carts
        //            .Include(i => i.CartItems)
        //            .ThenInclude(i => i.Product)
        //            .FirstOrDefault(i => i.UserId == userId);
        //}
       
    }
}
