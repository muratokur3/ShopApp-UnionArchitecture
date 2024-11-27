using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Models.DTOs.ProductDtos
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public int[] SelectedCategories { get; set; }
    }
}
