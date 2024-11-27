using ShopApp.Entity.Entities;

namespace ShopApp.Business.Models.VMs.ProductVms
{
    
    public class ProductListVm
    {
        public PageInfoVm PageInfo { get; set; }
        public List<ProductVm> Products { get; set; }

    }
}
