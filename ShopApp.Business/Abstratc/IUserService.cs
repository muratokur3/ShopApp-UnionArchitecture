using Microsoft.AspNetCore.Identity;
using ShopApp.Business.Models.DTOs.UserDtos;
using ShopApp.Business.Models.VMs.IdentityVms;
using ShopApp.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Abstratc;
public interface IUserService
{
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<User> FindByIdAsync(string userId);
    Task<User> FindByNameAsync(string userName);
    Task<SignInResult> PasswordSignInAsync(string userName, string password);
    Task SignOutAsync();
    Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
    Task<IList<string>> GetRolesAsync(User user);
}