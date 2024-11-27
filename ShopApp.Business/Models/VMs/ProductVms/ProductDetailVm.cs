using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Entity.Entities;

namespace ShopApp.Business.Models.VMs.ProductVms;

public class ProductDetailVm
{
    public ProductVm Product { get; set; }
    public List<CategoryVm> Categories { get; set; }
}
