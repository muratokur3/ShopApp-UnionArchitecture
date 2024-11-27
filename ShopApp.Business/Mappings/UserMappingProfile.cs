using AutoMapper;
using ShopApp.Business.Models.DTOs.ProductDtos;
using ShopApp.Business.Models.DTOs.UserDtos;
using ShopApp.Business.Models.VMs.IdentityVms;
using ShopApp.Business.Models.VMs.ProductVms;
using ShopApp.DataAccess.Identity;
using ShopApp.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Mappings;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserVm>();
        CreateMap<UserVm, User>();
       CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
        CreateMap<LoginDto, User>();
        CreateMap<User, LoginDto>();
        CreateMap<UserRoleDto, User>();
        CreateMap<User, UserRoleDto>();
       
    }
}
