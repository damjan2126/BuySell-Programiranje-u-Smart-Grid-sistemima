using BuyAndSell.Business.MappingProfiles;
using BuyAndSell.Business.Services;
using BuyAndSell.Business.Services.Contracts;

namespace BuySell.Host.Extensions
{
    public static class BusinessConfigurationExtension
    {
        public static void ConfigureBusiness(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddScoped<IUserService, UserService>();
        }
    }
}
