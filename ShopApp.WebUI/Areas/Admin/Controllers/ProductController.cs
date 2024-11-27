using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;
using ShopApp.WebUI.Extentions;
using System.Net.Http;
using System.Text;

namespace ShopApp.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public ProductController(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ProductList()
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        using (var response = await httpClient.GetAsync("Products"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductVm>>(apiResponse);
            return View(products);
        }
    }

    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductCreateDto model)
    {
        if (ModelState.IsValid)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync("Products", jsonContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    //TempData.Put("message", new AlertMessage()
                    //{
                    //    Title = "Success",
                    //    Message = $"{model.Name} was added successfully",
                    //    AlertType = "success"
                    //});
                    return RedirectToAction("ProductList");
                }
                else
                {
                    //TempData.Put("message", new AlertMessage()
                    //{
                    //    Title = "Error",
                    //    Message = "There was an error adding the product",
                    //    AlertType = "danger"
                    //});
                }
            }
        }

        return View(model);
    }

    public async Task<IActionResult> ProductEdit(int id)
    {
        var httpClient = _httpClientFactory.CreateClient("api");
        var product = new ProductUpdateDto();

        using (var response = await httpClient.GetAsync($"Products/{id}"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var productVm = JsonConvert.DeserializeObject<ProductVm>(apiResponse);
            product = new ProductUpdateDto()
            {
                ProductId = productVm.ProductId,
                Name = productVm.Name,
                Url = productVm.Url,
                Price = productVm.Price,
                Description = productVm.Description,
                ImageUrl = productVm.ImageUrl,
                IsApproved = productVm.IsApproved,
                IsHome = productVm.IsHome,
                SelectedCategories = productVm.Categories.Select(i => i.CategoryId).ToArray()
            };
        }

        using (var response = await httpClient.GetAsync("Category"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryVm>>(apiResponse);

            ViewBag.Categories = categories;
        }


        return View(product);
    }


    [HttpPost]
    public async Task<IActionResult> ProductEdit(ProductUpdateDto model, int[] categoryIds, IFormFile file)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        // Dosya yükleme işlemi, eğer dosya sağlanmışsa
        if (file != null)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Img", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            model.ImageUrl = file.FileName;
        }
        else
        {
            model.ImageUrl = "3.jpg"; // Varsayılan resim
        }

        // Kategorileri model'e ekle
        model.SelectedCategories = categoryIds.ToArray();

        // Ürünü güncelle
        var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PutAsync($"Products/{model.ProductId}", jsonContent))
        {
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Başarıyla güncellendi",
                    Message = $"{model.Name} başarıyla güncellendi",
                    AlertType = "success"
                });
                return RedirectToAction("ProductList");
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "Ürün güncellenirken bir hata oluştu",
                    AlertType = "danger"
                });
            }
        }

        // Eğer buraya geldiysek, bir şeyler yanlış gitti, formu yeniden göster
        var httpClientForCategories = _httpClientFactory.CreateClient("api");
        using (var response = await httpClientForCategories.GetAsync("Categories"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryVm>>(apiResponse);
            ViewBag.Categories = categories;
        }

        return View(model);
    }



    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        // Ürünü sil
        using (var response = await httpClient.DeleteAsync($"Products/{productId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Başarıyla silindi",
                    Message = $"Ürün başarıyla silindi",
                    AlertType = "success"
                });
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "Ürün silinirken bir hata oluştu",
                    AlertType = "danger"
                });
            }
        }

        return RedirectToAction("ProductList");
    }

}