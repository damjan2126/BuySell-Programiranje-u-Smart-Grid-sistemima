using BuyAndSell.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BuyAndSell.Data.Repositories.Contracts;
using BuyAndSell.Data.Repositories;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Repositories;

namespace BuyAndSell.Data.Extensions
{
    public static class ServiceCollection
    {
        public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<DatabaseContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DatabaseConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<DbInitializer>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserStatusRepository, UserStatusRepository>();
        }
    }
}
