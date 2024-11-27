using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web;

namespace ShopApp.WebUI.Controllers;

public class ShopController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    public ShopController(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> List(string category, int page = 1)
    {
        var pageSize = 3;
        var httpClient = _httpClientFactory.CreateClient("api");
        var queryString = new Dictionary<string, string>
    {
        { "name", category },
        { "page", page.ToString() },
        { "pageSize", pageSize.ToString() }
    };

        using (var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("Products/list", queryString)))
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ProductListVm>(apiResponse);
            return View(products);
        }
    }

    public async Task<IActionResult> Details(string url)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        using (var response = await httpClient.GetAsync($"Products/details/{url}"))
        {
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ProductVm>(apiResponse);

                if (product == null)
                {
                    return NotFound(); 
                }

                return View(product);
            }
            return NotFound();
           
        }

    }

    public async Task<IActionResult> Search(string q)
    {
        var httpClient = _httpClientFactory.CreateClient("api");

        using (var response = await httpClient.GetAsync($"Products/search/{q}"))
        {
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductVm>>(apiResponse);

                if (products == null)
                {
                    return NotFound();
                }

                return View(products);
            }
            return NotFound();

        }

    }
}
