using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopApp.Entity.Entities;
using ShopApp.Business.Models.DTOs.CategoryDtos;
using ShopApp.Business.Models.VMs.CategoryVms;

namespace ShopApp.Business.Mappings;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryVm>();
        CreateMap<CategoryVm, Category>();
        CreateMap<Category, CategoryCreateDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Category, CategoryUpdateDto>();
        CreateMap<CategoryUpdateDto, Category>();
    }
}
