using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
     
        Task<Product> GetProductDetails(string url);
        Task<Product> GetByIdWithCategories(int id);
        Task<List<Product>> GetSearchResult(string searchString);
        Task<List<Product>> GetHomePageProducts();
        Task<int>  GetCountByCategory(string category);
        Task<List<Product>> GetProductByCategory(string name,int page,int pageSize);
        void Update(Product entity, int[] categoryIds);
    }
}
