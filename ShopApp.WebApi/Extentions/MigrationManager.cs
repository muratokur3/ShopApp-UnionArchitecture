﻿using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Concrete.EfCore;

namespace ShopApp.WebApi.Extentions
{
    public static class MigrationManager
    {
        public static IHost ApplyPendingMigrations(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        applicationContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occured while migrating the database.");
                    }
                }
                using (var shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>())
                {
                    try
                    {
                        shopContext.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occured while migrating the database.");
                    }
                }
                return host;

            }
        }
    }
}
