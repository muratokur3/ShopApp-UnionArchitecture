using ShopApp.DataAccess.Abstract;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore;

public class EfCoreProductCategoryRepository:EfcoreGenericRepository<ProductCategory>,IProductCategoryRepository
{
    public EfCoreProductCategoryRepository(ShopContext context) : base(context)
    {
    }
    private ShopContext ShopContext
    {
        get { return context as ShopContext; }
    }
}
