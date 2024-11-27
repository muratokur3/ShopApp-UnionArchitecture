using AutoMapper;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.CategoryDtos;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        public CategoryManager(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public string ErrorMessage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<int> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _unitofwork.Categories.CreateAsync(category);
            await _unitofwork.saveAsync();
            return category.CategoryId;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitofwork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }
            await _unitofwork.Categories.DeleteAsync(category);
            await _unitofwork.saveAsync();
        }

        public async Task<List<CategoryVm>> GetAllAsync()
        {
            var cateories = await _unitofwork.Categories.GetAllAsync();
            return _mapper.Map<List<CategoryVm>>(cateories);
        }

        public async Task<CategoryVm> GetByIdAsync(int id)
        {
            return _mapper.Map<CategoryVm>(await _unitofwork.Categories.GetByIdAsync(id));
        }

        public async Task<CategoryVm> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
        {
            // Mevcut kategoriyi veritabanından al
            var existingCategory = await _unitofwork.Categories.GetByIdAsync(categoryUpdateDto.CategoryId);
            if (existingCategory == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            // Mevcut kategoriyi güncelle
            _mapper.Map(categoryUpdateDto, existingCategory);

            // Güncellenmiş kategoriyi veritabanına kaydet
            await _unitofwork.saveAsync();

            // Sonucu geri döndür
            var result = _mapper.Map<CategoryVm>(existingCategory);
            return result;
        }

        //public void Create(Category entity)
        //{
        //    _unitofwork.Categories.Create(entity);
        //    _unitofwork.save();
        //}
        //public async Task<Category> CreateAsync(Category entity)
        //{

        //    await _unitofwork.Categories.CreateAsync(entity);
        //    await _unitofwork.saveAsync();
        //    return entity;
        //}
        //public void Delete(Category entity)
        //{
        //    _unitofwork.Categories.Delete(entity);
        //    _unitofwork.save();
        //}

        //public void DeleteFromCategory(int productId, int categoryId)
        //{
        //     _unitofwork.Categories.DeleteFromCategory(productId, categoryId);
        //}

        //public async Task<List<Category>> GetAll()
        //{
        //    return await _unitofwork.Categories.GetAll();
        //}

        //public async Task<Category> GetById(int id)
        //{
        //    return  await _unitofwork.Categories.GetById(id);
        //}

        //public Category GetByIdWithProducts(int categoryId)
        //{
        //    return  _unitofwork.Categories.GetByIdWithProducts(categoryId);
        //}

        //public void Update(Category entity)
        //{
        //      _unitofwork.Categories.Update(entity);
        //    _unitofwork.save();
        //}

        //public bool Validate(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
