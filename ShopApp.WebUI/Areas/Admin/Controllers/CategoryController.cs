using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.CategoryDtos;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs;
using ShopApp.Business.Models.VMs.CartVms;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;
using ShopApp.WebUI.Extentions;
using System.Net.Http;
using System.Text;

namespace ShopApp.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{

    private readonly IHttpClientFactory _httpClientFactory;
    public CategoryController(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> CategoryList()
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        using (var response = await httpClient.GetAsync("Category"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<CategoryVm>>(apiResponse);
            return View(categories);
        }
    }
    public IActionResult CategoryCreate()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int CategoryId)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        // Ürünü sil
        using (var response = await httpClient.DeleteAsync($"Category/{CategoryId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "was deleted successfully",
                    AlertType = "danger",
                    Message = " was deleted successfully"
                });
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "Category silinirken bir hata oluştu",
                    AlertType = "danger"
                });
            }
        }
        return RedirectToAction("Categorylist");
    }

    [HttpPost]
    public async Task<IActionResult> CategoryCreate(CategoryCreateDto model)
    {
        if (ModelState.IsValid)
        {
            var httpClient = _httpClientFactory.CreateClient("api");
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync("Category", jsonContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "was added successfully",
                        AlertType = "success",
                        Message = $"{model.Name} was added successfully"
                    });
                    return RedirectToAction("CategoryList");
                }
                else
                {
                    TempData.Put("message", new AlertMessage()
                    {
                        Title = "Error",
                        Message = "There was an error adding the category",
                        AlertType = "danger"
                    });
                }
            }
        }

        return View(model);
    }

    public async Task<IActionResult> CategoryEdit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var httpClient = _httpClientFactory.CreateClient("api");


        using (var response = await httpClient.GetAsync($"Category/{id}"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<CategoryUpdateDto>(apiResponse);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CategoryEdit(CategoryUpdateDto model)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        
        var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        using (var response = await httpClient.PutAsync($"Category/{model.CategoryId}", jsonContent))
        {
            if (response.IsSuccessStatusCode)
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Başarıyla güncellendi",
                    Message = $"{model.Name} başarıyla güncellendi",
                    AlertType = "success"
                });
                return RedirectToAction("CategoryList");
            }
            else
            {
                TempData.Put("message", new AlertMessage()
                {
                    Title = "Hata",
                    Message = "Category güncellenirken bir hata oluştu",
                    AlertType = "danger"
                });
                return View(model);
            }
        }

        
    }

}

//    [HttpPost]
//    public async Task<Iactionresult> Categoryedit(CategoryProductVm model)
//{
//    if (modelstate.ısvalid)
//    {
//        var entity = await _categoryservice.getbyıd(model.categoryıd);
//        if (entity == null)
//        {
//            return notfound();
//        }
//        entity.name = model.name;
//        entity.url = model.url;

//        _categoryservice.update(entity);


//        tempdata.put("message", new alertmessage()
//        {
//            title = "was updated successfully",
//            message = $"{entity.name} isimli category güncellendi.",
//            alerttype = "success"
//        });
//        return redirecttoaction("categorylist");
//    }
//    return view(model);
//} }



//    public IActionResult CategoryEdit(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        var entity = _categoryService.GetByIdWithProducts((int)id);

//        if (entity == null)
//        {
//            return NotFound();
//        }

//        var model = new CategoryModel()
//        {
//            CategoryId = entity.CategoryId,
//            Name = entity.Name,
//            Url = entity.Url,
//            Products = entity.ProductCategories.Select(p => p.Product).ToList()
//        };
//        return View(model);
//    }

//    [HttpPost]
//    public async Task<IActionResult> CategoryEdit(CategoryModel model)
//    {
//        if (ModelState.IsValid)
//        {
//            var entity = await _categoryService.GetById(model.CategoryId);
//            if (entity == null)
//            {
//                return NotFound();
//            }
//            entity.Name = model.Name;
//            entity.Url = model.Url;

//            _categoryService.Update(entity);


//            TempData.Put("message", new AlertMessage()
//            {
//                Title = "was updated successfully",
//                Message = $"{entity.Name} isimli category güncellendi.",
//                AlertType = "success"
//            });
//            return RedirectToAction("CategoryList");
//        }
//        return View(model);
//    }
//    public async Task<IActionResult> DeleteCategory(int CategoryId)
//    {
//        var entity = await _categoryService.GetById(CategoryId);
//        if (entity != null)
//        {
//            _categoryService.Delete(entity);
//        }

//        TempData.Put("message", new AlertMessage()
//        {
//            Title = "was deleted successfully",
//            AlertType = "danger",
//            Message = $"{entity.Name} was deleted successfully"
//        });
//        return RedirectToAction("CategoryList");
//    }

//    public IActionResult DeleteFromCategory(int productId, int categoryId)
//    {
//        _categoryService.DeleteFromCategory(productId, categoryId);
//        return Redirect("/admin/categoryedit/" + categoryId);
//    }
