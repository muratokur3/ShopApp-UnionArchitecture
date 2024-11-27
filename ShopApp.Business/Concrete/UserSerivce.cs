using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopApp.Business.Abstratc;
using ShopApp.Business.Models.DTOs.UserDtos;
using ShopApp.Business.Models.VMs.IdentityVms;
using ShopApp.DataAccess.Abstract;
using ShopApp.DataAccess.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Business.Concrete;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userRepository.CreateUserAsync(user, password);
    }

    public async Task<User> FindByIdAsync(string userId)
    {
        return await _userRepository.FindByIdAsync(userId);
    }

    public async Task<User> FindByNameAsync(string userName)
    {
        return await _userRepository.FindByNameAsync(userName);
    }

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password)
    {
        return await _userRepository.PasswordSignInAsync(userName, password);
    }

    public async Task SignOutAsync()
    {
        await _userRepository.SignOutAsync();
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
    {
        return await _userRepository.AddToRoleAsync(user, roleName);
    }

    public async Task<IdentityResult> RemoveFromRoleAsync(User user, string roleName)
    {
        return await _userRepository.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        return await _userRepository.GetRolesAsync(user);
    }
}