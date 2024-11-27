using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Models.VMs.CategoryVms;

public class CategoryProductVm
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public List<Product> Products { get; set; }
}
