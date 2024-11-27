using Azure;
using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreProductRepository : EfcoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(ShopContext context) : base(context)
        {
        }
        private ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }
        //product bilgilerini ve categori bilgilerini getirir
        public async Task<Product> GetProductDetails(string url)
        {

            return await ShopContext.Products
    .Where(i => i.Url == url)
    .Include(i => i.ProductCategories)
    .ThenInclude(i => i.Category)
    .FirstOrDefaultAsync();


        }
        public async Task<Product> GetByIdWithCategories(int id)
        {
            return await ShopContext.Products
                             .Where(i => i.ProductId == id)
                             .Include(i => i.ProductCategories)
                             .ThenInclude(i => i.Category)
                             .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetSearchResult(string searchString)
        {
            var products = ShopContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(i => i.IsApproved && (i.Name.ToLower().Contains(searchString.ToLower()) || i.Description.ToLower().Contains(searchString.ToLower())));
            }
            return await products.ToListAsync();
        }
        public async Task<List<Product>> GetHomePageProducts()
        {

            return await ShopContext.Products.Where(i => i.IsHome).ToListAsync();
        }
        public async Task<int> GetCountByCategory(string category)
        {
            return await ShopContext.Products
                .Where(p => p.ProductCategories.Any(pc => pc.Category.Name == category))
                .CountAsync();
        }

        public async Task<List<Product>> GetProductByCategory(string name, int page, int pageSize)
        {
            var products = ShopContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                products = products
                            .Include(i => i.ProductCategories)
                            .ThenInclude(i => i.Category)
                            .Where(i => i.ProductCategories.Any(a => a.Category.Url == name));
            }
            return await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public void Update(Product entity, int[] categoryIds)
        {
            var product = ShopContext.Products
                .Include(i => i.ProductCategories)
                .FirstOrDefault(i => i.ProductId == entity.ProductId);

            if (product != null)
            {
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Description = entity.Description;
                product.Url = entity.Url;
                product.ImageUrl = entity.ImageUrl;
                product.IsApproved = entity.IsApproved;
                product.IsHome = entity.IsHome;

                // Mevcut kategorileri al
                var existingCategories = product.ProductCategories.ToList();

                // Silinmesi gereken kategorileri bul
                var categoriesToRemove = existingCategories.Where(ec => !categoryIds.Contains(ec.CategoryId)).ToList();

                // Eklenmesi gereken kategorileri bul
                var categoriesToAdd = categoryIds.Where(cid => !existingCategories.Any(ec => ec.CategoryId == cid))
                    .Select(catid => new ProductCategory()
                    {
                        ProductId = entity.ProductId,
                        CategoryId = catid
                    }).ToList();

                // Silinmesi gereken kategorileri kaldır
                foreach (var categoryToRemove in categoriesToRemove)
                {
                    product.ProductCategories.Remove(categoryToRemove);
                    ShopContext.ProductCategories.Remove(categoryToRemove); // İlişkiyi veritabanından da kaldır
                }

                // Eklenmesi gereken kategorileri ekle
                foreach (var categoryToAdd in categoriesToAdd)
                {
                    product.ProductCategories.Add(categoryToAdd);
                }

                ShopContext.SaveChanges();
            }
        }
    }
}



//if (entity != null)
//{
//    entity.ProductCategories = categoryIds.Select(catid => new ProductCategory()
//    {
//        ProductId = entity.ProductId,
//        CategoryId = catid
//    }).ToList();
//}