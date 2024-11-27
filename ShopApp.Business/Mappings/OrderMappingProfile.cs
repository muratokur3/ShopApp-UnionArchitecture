using AutoMapper;
using ShopApp.Business.Models.DTOs.OrderDtos;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Mappings;

public class OrderMappingProfile: Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<Order, OrderCreateDto>();
        CreateMap<Order, OrderUpdateDto>();
        
    }
}
