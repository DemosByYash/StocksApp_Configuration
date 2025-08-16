using StocksApp_Configuration.Configuration;
using StocksApp_Configuration.Services;
using StocksApp_Configuration.ServicesContracts; 


namespace StocksApp_Configuration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<StocksService>();
            builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            app.Run();
        }
    }
}
