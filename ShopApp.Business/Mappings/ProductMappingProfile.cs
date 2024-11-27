using AutoMapper;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.VMs.CategoryVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>()
              .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));

        CreateMap<ProductDto, Product>();
        CreateMap<Product, ProductCreateDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<Product, ProductUpdateDto>();
        CreateMap<ProductUpdateDto, Product>();

        CreateMap<Product, ProductVm>()//product içindeki category'leri categoryvm'e atar
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.ProductCategories.Select(pc => pc.Category)));
     

        CreateMap<ProductVm, Product>();
        CreateMap<ProductUpdateDto, ProductVm>();
        CreateMap<ProductVm, ProductUpdateDto>()//categoryvm içindeki categoryId değerlerini categoryupdatedto içindeki selectedcategories'e atar
            .ForMember(dest => dest.SelectedCategories, opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryId).ToArray()));
        CreateMap<Product, ProductDetailVm>();
        CreateMap<ProductDetailVm, Product>();
        CreateMap<Product, ProductListVm>();
        CreateMap<ProductListVm, Product>();

    }
}
