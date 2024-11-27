using Microsoft.AspNetCore.Identity;
using ShopApp.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.Abstract;

public interface IUserRepository
{


    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<User> FindByIdAsync(string userId);
    Task<User> FindByNameAsync(string userName);
    Task<SignInResult> PasswordSignInAsync(string userName, string password);
    Task SignOutAsync();
    Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);
    Task<IList<string>> GetRolesAsync(User user);

    //public Task<User> FindByNameAsync(string userName);
    //public Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    //public Task<User> FindByIdAsync(string id);
    //public Task<List<User>> GetUsersAsync();
    //public Task<bool> IsInRoleAsync(User user, string roleName);
    //public Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName);

    //public Task<IdentityResult> CreateUserAsync(User user, string password);

    //Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    //Task<bool> CheckPasswordAsync(AppUser user, string password);
    //Task<IList<string>> GetRolesAsync(AppUser user);
    //Task<bool> IsInRoleAsync(AppUser user, string role);
    //Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string role);
    //Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    //Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
    //Task<string> GenerateEmailConfirmationTokenAsync(string token);
}
