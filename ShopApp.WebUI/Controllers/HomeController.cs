using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.VMs.ProductVms;
using System.Diagnostics;
using System.Text.Json;

namespace ShopApp.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public HomeController(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }


    public async Task<IActionResult> Index()
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        using (var response = await httpClient.GetAsync("Products/home"))
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductVm>>(apiResponse);
            return View(products);
        }

    }


    //public IActionResult About()
    //{
    //    return View();
    //}

    //public IActionResult Contact()
    //{

    //    return View();
    //}



}
