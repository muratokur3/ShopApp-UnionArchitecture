using ShopApp.Business.Models.DTOs.CategoryDtos;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstratc
{
    public interface ICategoryService
        //:IValidator<Category>
    {
        Task<List<CategoryVm>> GetAllAsync();
        Task<CategoryVm> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryVm> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);




        //Category GetByIdWithProducts(int categoryId);
        //Task<Category> GetById(int id);
        //void DeleteFromCategory(int productId, int categoryId);
        //Task<List<Category>> GetAll();
        //void Create(Category entity);
        //Task<Category> CreateAsync(Category entity);

        //void Update(Category entity);
        //void Delete(Category entity);

    }
}
