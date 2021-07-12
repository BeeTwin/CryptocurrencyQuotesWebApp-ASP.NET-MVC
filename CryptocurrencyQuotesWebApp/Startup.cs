using CryptocurrencyQuotesLibrary.Data.Contexts;
using CryptocurrencyQuotesLibrary.BusinessLogic.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz.Impl;
using CryptocurrencyQuotesLibrary.BusinessLogic.ApiCallers;
using CryptocurrencyQuotesLibrary.BusinessLogic.JsonConverters;
using CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers.DataUpdaters;
using System.Net;
using CryptocurrencyQuotesLibrary.BusinessLogic.Schedulers;
using CryptocurrencyQuotesLibrary.Models.DbModels;

namespace CryptocurrencyQuotesWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddDependencies(services);

            services.AddDbContext<UsersAuthDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(DbConnectionStrings.USERS_AUTH),
                    b => b.MigrationsAssembly("CryptocurrencyQuotesMigrations")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<CryptocurrencyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(DbConnectionStrings.CRYPTOCURRENCY),
                    b => b.MigrationsAssembly("CryptocurrencyQuotesMigrations")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<UsersAuthDbContext>();
            services.AddControllersWithViews();
        }

        private void AddDependencies(IServiceCollection services)
        {
            services.AddScoped<IApiCaller, CoinmarketcapApiCaller>();
            services.AddScoped<IDataUpdater, DataUpdater>();
            services.AddScoped<IJsonConverter<List<Cryptocurrency>>, CryptocurrencyJsonConverter>();
            services.AddScoped<JobFactory>();
            services.AddScoped<DataUpdateJob>();

            var client = new WebClient();
            services.AddSingleton(client);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
