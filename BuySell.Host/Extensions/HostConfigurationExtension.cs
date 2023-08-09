using System.Text.Json.Serialization;
using BuySell.Host.Middlewares;

namespace BuySell.Host.Extensions
{
    public static class HostConfigurationExtension
    {
        /// <summary>
        /// Services configuration
        /// </summary>
        public static void ConfigureHost(this WebApplicationBuilder builder)
        {
            builder.ConfigureSwagger();

            var services = builder.Services;
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddEndpointsApiExplorer();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.ConfigureAuth();
        }

        /// <summary>
        /// App configuration
        /// </summary>
        public static void ConfigureHost(this WebApplication app)
        {
            app.MapControllers();

            app.SetupData();

            // CORS
            app.UseCors("AllowAll");

            // custom jwt auth middleware

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCustomExceptionHandler();

            app.UseStaticFiles();

            app.ConfigureSwagger();
        }
    }
}
