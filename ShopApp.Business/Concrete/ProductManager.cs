using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Concrete.EfCore;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;

namespace ShopApp.Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductManager> _logger;
        public ProductManager(IUnitOfWork unitofwork, IMapper mapper, ILogger<ProductManager> logger)
        {
            _unitOfWork = unitofwork;
            _mapper = mapper;
            _logger = logger;

        }


        public async Task<List<ProductVm>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return _mapper.Map<List<ProductVm>>(products);
        }


        public async Task<ProductVm> GetByIdAsync(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return _mapper.Map<ProductVm>(product);
        }

        public async Task<int> CreateAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            await _unitOfWork.Products.CreateAsync(product);
            await _unitOfWork.saveAsync();
            return product.ProductId;
        }

        
        public async Task DeleteAsync(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id);
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                await _unitOfWork.Products.DeleteAsync(product);
                await _unitOfWork.saveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                throw;
            }
        }

        public async Task<ProductVm> GetProductDetailsAsync(string url)
        {
            var product = await _unitOfWork.Products.GetProductDetails(url);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            var productVm = _mapper.Map<ProductVm>(product);

            return productVm;
        }


        public async Task<ProductVm> GetByIdWithCategories(int id)
        {
           var product = await _unitOfWork.Products.GetByIdWithCategories(id);
            return _mapper.Map<ProductVm>(product);
        }

        public async Task<List<ProductVm>> GetSearchResult(string searchString)
        {
            var products = await _unitOfWork.Products.GetSearchResult(searchString);
            return _mapper.Map<List<ProductVm>>(products);
        }


        public async Task<List<ProductVm>> GetHomePageProducts()
        {
            var products = await _unitOfWork.Products.GetHomePageProducts();
            return _mapper.Map<List<ProductVm>>(products);
        }

        public async Task<int> GetCountByCategory(string category)
        {
            return await _unitOfWork.Products.GetCountByCategory(category);

        }
        public async Task<ProductListVm> GetProductByCategory(string name, int page, int pageSize)
        {
            var products = await _unitOfWork.Products.GetProductByCategory(name, page, pageSize);
            var productVm = _mapper.Map<List<ProductVm>>(products);
            var totalItems = await _unitOfWork.Products.GetCountByCategory(name);

            var pageInfo = new PageInfoVm
            {
                TotalItems = totalItems,
                ItemsPerPage = pageSize,
                CurrentPage = page
            };

            var productList= new ProductListVm
            {
                Products = productVm,
                PageInfo = pageInfo
            };
            return productList;
        }

        public async Task UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            _unitOfWork.Products.Update(_mapper.Map<Product>(productUpdateDto), productUpdateDto.SelectedCategories);
        }
    }










        //public Product GetProductDetails(string url)
        //{
        //    return _unitofwork.Products.GetProductDetails(url);
        //}



        //public List<ProductVm> GetProductByCategory(string name, int page, int pageSize)
        //{
        //    var products = _unitOfWork.Products.GetProductByCategory(name, page, pageSize);
        //    return _mapper.Map<List<ProductVm>>(products);

        //}

        //public List<Product> GetHomePageProducts()
        //{
        //    return _unitOfWork.Products.GetHomePageProducts();
        //}

        //public List<Product> GetSearchResult(string searchString)
        //{
        //    return _unitOfWork.Products.GetSearchResult(searchString);
        //}




        //public string ErrorMessage { get; set; }
        //public bool Validate(Product entity)
        //{
        //    var isValid = true;
        //    if (string.IsNullOrEmpty(entity.Name))
        //    {
        //        ErrorMessage += "Ürün ismi girin. \n";
        //        isValid = false;
        //    }
        //    if (entity.Price<5)
        //    {
        //        ErrorMessage += "Fiyat negatif olamaz. \n";
        //        isValid = false;
        //    }
        //    return isValid;
        //}




    
}
