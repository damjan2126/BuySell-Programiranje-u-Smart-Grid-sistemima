using BuyAndSell.Data.Extensions;

namespace BuySell.Host.Extensions
{
    public static class WebAppExtension
    {
        public static void SetupData(this WebApplication app)
        {
            var serviceScope = app.Services.CreateScope();
            serviceScope.SetupDataAsync();
        }
    }
}
