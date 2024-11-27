public static class RouteConfig
{
    public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
            name: "adminusers",
            pattern: "admin/user/list",
            defaults: new { area = "Admin", controller = "User", action = "UserList" });

        endpoints.MapControllerRoute(
            name: "adminuseredit",
            pattern: "admin/user/{id?}",
            defaults: new { area = "Admin", controller = "User", action = "UserEdit" });

        endpoints.MapControllerRoute(
            name: "adminroles",
            pattern: "admin/role/list",
            defaults: new { area = "Admin", controller = "Role", action = "RoleList" });

        endpoints.MapControllerRoute(
            name: "adminroles",
            pattern: "admin/role/create",
            defaults: new { area = "Admin", controller = "Role", action = "RoleCreate" });

        endpoints.MapControllerRoute(
            name: "adminroleedit",
            pattern: "admin/role/{id?}",
            defaults: new { area = "Admin", controller = "Role", action = "RoleEdit" });

        endpoints.MapControllerRoute(
            name: "admincategorycreate",
            pattern: "admin/categories/create",
            defaults: new { area = "Admin", controller = "Category", action = "CategoryCreate" });

        endpoints.MapControllerRoute(
            name: "admincategories",
            pattern: "admin/categories",
            defaults: new { area = "Admin", controller = "Category", action = "CategoryList" });

        endpoints.MapControllerRoute(
            name: "admincategoryedit",
            pattern: "admin/categories/{id?}",
            defaults: new { area = "Admin", controller = "Category", action = "CategoryEdit" });

        endpoints.MapControllerRoute(
            name: "adminproductcreate",
            pattern: "admin/products/create",
            defaults: new { area = "Admin", controller = "Product", action = "ProductCreate" });

        endpoints.MapControllerRoute(
            name: "adminproductlist",
            pattern: "admin/products",
            defaults: new { area = "Admin", controller = "Product", action = "ProductList" });

        endpoints.MapControllerRoute(
            name: "adminproductedit",
            pattern: "admin/products/{id?}",
            defaults: new { area = "Admin", controller = "Product", action = "ProductEdit" });

        endpoints.MapControllerRoute(
            name: "order",
            pattern: "order",
            defaults: new { controller = "Order", action = "Index" });

        endpoints.MapControllerRoute(
            name: "checkout",
            pattern: "checkout",
            defaults: new { controller = "Cart", action = "Checkout" });

        endpoints.MapControllerRoute(
            name: "cart",
            pattern: "cart",
            defaults: new { controller = "Cart", action = "Index" });

        endpoints.MapControllerRoute(
            name: "Products",
            pattern: "products/{category?}",
            defaults: new { controller = "shop", action = "List" });

        endpoints.MapControllerRoute(
            name: "search",
            pattern: "search",
            defaults: new { controller = "shop", action = "Search" });

        endpoints.MapControllerRoute(
            name: "productdetails",
            pattern: "{url}",
            defaults: new { controller = "shop", action = "Details" });

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}