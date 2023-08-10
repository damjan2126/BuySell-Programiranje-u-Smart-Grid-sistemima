using BuySell.Data;
using BuySell.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Extensions
{
    public static class ServiceScope
    {
        public static async Task SetupDataAsync(this IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            if (dbContext.Database.GetPendingMigrations().Any()) dbContext.Database.Migrate();
            var userManager = (UserManager<User>)scope.ServiceProvider.GetService(typeof(UserManager<User>))!;
            var roleManager = (RoleManager<Role>)scope.ServiceProvider.GetService(typeof(RoleManager<Role>))!;


            var dbInitializerInstance = new DbInitializer(userManager, dbContext, roleManager);
            await dbInitializerInstance.InitializeAsync();
        }
    }
}
