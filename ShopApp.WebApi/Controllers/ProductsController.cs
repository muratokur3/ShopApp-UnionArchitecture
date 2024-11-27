using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;

namespace ShopApp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private IProductService _productService;
    private ICategoryService _categoryService;
    private IMapper _mapper;
    public ProductsController(IProductService productService,ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }




    [HttpGet]
    public async Task<ActionResult<List<ProductVm>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        return products;
    }

    [HttpGet("list")]
    public async Task<ActionResult<ProductListVm>> GetProductByCategory(string name = null, int page = 1, int pageSize = 2)
    {
        var products = await _productService.GetProductByCategory(name, page, pageSize);
        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductVm>> GetProduct(int id)
    {
        var product = await _productService.GetByIdWithCategories(id);
        return product;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductCreateDto productCreateDto)
    {
        var productId = await _productService.CreateAsync(productCreateDto);
        var product = await _productService.GetByIdAsync(productId);
        return CreatedAtAction(nameof(GetProduct), new { id = productId }, product);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto model)
    {
        if (id != model.ProductId)
        {
            return BadRequest();
        }
        await _productService.UpdateAsync(model);
        return NoContent();
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("details/{url}")]
    public async Task<IActionResult> GetProductDetails(string url)
    {
        var productDetail = await _productService.GetProductDetailsAsync(url);
        if (productDetail == null)
        {
            return NotFound();
        }
        return Ok(productDetail);
    }

    [HttpGet("search/{searchString}")]
    public async Task<IActionResult> GetSearchResult(string searchString)
    {
        var products = await _productService.GetSearchResult(searchString);
        return Ok(products);
    }

    [HttpGet("home")]
    public async Task<IActionResult> GetHomePageProducts()
    {
        var products = await _productService.GetHomePageProducts();
        return Ok(products);
    }

   
}
