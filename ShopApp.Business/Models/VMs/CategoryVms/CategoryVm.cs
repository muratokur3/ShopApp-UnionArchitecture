using ShopApp.Entity.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Business.Models.VMs.CategoryVms
{
    public class CategoryVm
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
