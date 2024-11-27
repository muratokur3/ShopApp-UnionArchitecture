using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Business.Models.VMs.ProductVms
{
    public class ProductVm
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public string ImageUrl { get; set; }
        public List<CategoryVm> Categories { get; set; }
    }
}
