using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopApp.Business.Models.DTOs;
using ShopApp.Entity.Entities;
using ShopApp.Business.Models.DTOs.CartDtos;

namespace ShopApp.Business.Mappings;

public class CartMappingProfile:Profile
{
    public CartMappingProfile()
    {
        CreateMap<Cart, CartDto>();
        CreateMap<Cart, CartCreateDto>();
        CreateMap<Cart, CartUpdateDto>();

    }
}
