using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;


namespace ShopApp.WebUI.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {

        private readonly HttpClient _httpClient;
        public CategoriesViewComponent(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (RouteData.Values["category"] != null)
                ViewBag.SelectedCategory = RouteData?.Values["category"];
            var categories = new List<CategoryVm>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7207/api/category"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<CategoryVm>>(apiResponse);
                }
            }
            return View(categories);
            //return View(await _categoryServices.GetAll());
        }

    }
}
