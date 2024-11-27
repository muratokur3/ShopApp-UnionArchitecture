using ShopApp.Business.Abstratc;
using ShopApp.Business.Concrete;
using Autofac;

namespace ShopApp.Business.IoC;

public class DependencyResolver:Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<CartManager>().As<ICartService>().SingleInstance();
        builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();


    }
}
