using BuySell.Business.Services.Contracts;
using BuySell.Business.Services;
using BuySell.Business.MappingProfiles;

namespace BuySell.Host.Extensions
{
    public static class BusinessConfigurationExtension
    {
        public static void ConfigureBusiness(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddAutoMapper(typeof(UserMappingProfile));

            services.AddSingleton<IImageService, ImageService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IScopedEmailService, ScopedEmailService>();

            services.AddHostedService<TimedEmailService>();
        }
    }
}
