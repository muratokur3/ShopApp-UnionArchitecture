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
    public class EfcoreGenericRepository<TEntity> : IRepository<TEntity>
       where TEntity : class
    {
        protected readonly DbContext context;
        public EfcoreGenericRepository(DbContext ctx)
        {
            this.context = ctx;
        }
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public async Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }
        public async Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
        }
        public async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

    }
}