using CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers;
using CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptocurrencyQuotesWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ApiHelper.Initialize();
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    DataUpdateScheduler.Start(serviceProvider);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
