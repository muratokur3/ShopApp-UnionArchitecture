﻿using ShopApp.Business.Models.DTOs.CategoryDtos;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Models.DTOs.ProductDtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public List<CategoryVm> Categories { get; set; }
    }
}
