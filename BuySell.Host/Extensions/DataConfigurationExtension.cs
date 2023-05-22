using BuyAndSell.Data.Extensions;

namespace BuySell.Host.Extensions
{
    public static class DataConfigurationExtension
    {
        /// <summary>
        /// Services configuration
        /// </summary>
        public static void ConfigureData(this WebApplicationBuilder builder)
        {
            var services = builder.Services;
            services.ConfigureData(builder.Configuration);
        }

        /// <summary>
        /// App configuration
        /// </summary>
        public static void ConfigureData(this WebApplication app)
        {
            //
        }
    }
}
