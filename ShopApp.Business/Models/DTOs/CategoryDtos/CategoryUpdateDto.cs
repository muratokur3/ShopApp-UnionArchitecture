using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Models.DTOs.CategoryDtos
{
    public class CategoryUpdateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
