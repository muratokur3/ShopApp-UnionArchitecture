using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstratc;

public interface IProductService
{
    Task<List<ProductVm>> GetAllAsync();
    Task<ProductVm> GetByIdAsync(int id);
    Task<int> CreateAsync(ProductCreateDto productCreateDto);
    Task UpdateAsync(ProductUpdateDto productUpdateDto);
    Task DeleteAsync(int id);

    Task<ProductVm> GetProductDetailsAsync(string url);
    Task<ProductVm> GetByIdWithCategories(int id);
    Task<List<ProductVm>> GetSearchResult(string searchString);
    Task<List<ProductVm>> GetHomePageProducts();

    Task<int> GetCountByCategory(string category);
    Task<ProductListVm> GetProductByCategory(string name, int page, int pageSize);


    //Product GetProductDetails(string url);
    //List<ProductVm> GetProductByCategory(string name, int page, int pageSize);
    //List<Product> GetHomePageProducts();
    //List<Product> GetSearchResult(string searchString);



    //Task<ProductVm> GetById(int id);
    //Task<List<ProductVm>> GetAll();
    //Task Create(ProductCreateDto entity);
    //Task Update(Product entityToUpdate,Product entity);
    //Task Delete(Product entity);
    //int GetCountByCategory(string category);
    //bool Update(Product entity, int[] categoryIds);
}
