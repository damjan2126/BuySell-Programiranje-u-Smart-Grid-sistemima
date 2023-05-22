using BuySell.Host.Extensions;

namespace BuySell.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureHost();
            builder.ConfigureData();
            builder.ConfigureBusiness();

            var app = builder.Build();
            app.ConfigureHost();
            app.ConfigureData();

            app.Run();
        }
    }
}