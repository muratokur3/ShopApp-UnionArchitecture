using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Identity;


namespace ShopApp.WebUI.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        //private IProductService _productService;
        //private ICategoryService _categoryService;
        //private RoleManager<IdentityRole> _roleManager;
        //private UserManager<User> _userManager;
        //public HomeController(
        //                        IProductService productService,
        //                        ICategoryService categoryService,
        //                        RoleManager<IdentityRole> roleManager,
        //                        UserManager<User> userManager
        //                        )
        //{
        //    _productService = productService;
        //    _categoryService = categoryService;
        //    _roleManager = roleManager;
        //    _userManager = userManager;
        //}

        public IActionResult Index()
        {
            return View();
        }


    }
}


