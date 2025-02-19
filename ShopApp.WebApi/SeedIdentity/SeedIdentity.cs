﻿using Microsoft.AspNetCore.Identity;
using ShopApp.Business.Abstratc;
using ShopApp.DataAccess.Identity;
namespace ShopApp.WebApi.SeedIdentity
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ICartService cartService, IConfiguration configuration, ILogger logger)
        {
            var roles = configuration.GetSection("Data:Roles").GetChildren().Select(x => x.Value).ToArray();

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }
            }

            var users = configuration.GetSection("Data:Users");
            foreach (var section in users.GetChildren())
            {
                var username = section.GetValue<string>("UserName");
                var password = section.GetValue<string>("Password");
                var firstname = section.GetValue<string>("FirstName");
                var lastname = section.GetValue<string>("LastName");
                var email = section.GetValue<string>("Email");
                var role = section.GetValue<string>("Role");

                if (await userManager.FindByNameAsync(username) == null)
                {
                    var user = new User()
                    {
                        UserName = username,
                        FirstName = firstname,
                        LastName = lastname,
                        Email = email,
                        EmailConfirmed = true,
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                        //cartService.initializeCart(user.Id);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            logger.LogError($"Error creating user {username}: {error.Description}");
                        }
                    }
                }
            }
        }
    }
}
