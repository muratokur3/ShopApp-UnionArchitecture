using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.CategoryDtos;

namespace ShopApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {


        private ICategoryService _categoryService;
        private IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            // eğer isim null veya boş gleirse hata döndür
            if (string.IsNullOrEmpty(categoryCreateDto.Name))
            {
                return BadRequest("Category name is required");
            }
            if (string.IsNullOrEmpty(categoryCreateDto.Url))
            {
                return BadRequest("Category Url is required");
            }
            var categoryId = await _categoryService.CreateAsync(categoryCreateDto);
            var category = await _categoryService.GetByIdAsync(categoryId);
            return CreatedAtAction(nameof(GetCategory), new { id = categoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id != categoryUpdateDto.CategoryId)
            {
                return BadRequest("Category ID mismatch");
            }
            try
            {
                var updatedCategory = await _categoryService.UpdateAsync(categoryUpdateDto);
                return Ok(updatedCategory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
