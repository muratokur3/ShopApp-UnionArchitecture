﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        IProductCategoryRepository ProductCategory { get; }
        ICartRepository Carts { get; }
        IOrderRepository Orders { get; }
    

        Task<int> saveAsync();
    }
}
