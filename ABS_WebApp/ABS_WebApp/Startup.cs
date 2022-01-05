using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using System.Linq;

using ABS_WebApp.Services;
using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.Models;

using static ABS_DataConstants.DataConstrain;
using System.Net.Http;

namespace ABS_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<ICookieContainerAccessor, DefaultCookieContainerAccessor>();

            services.AddHttpClient<WebApiService>()
                .ConfigurePrimaryHttpMessageHandler(sp => new HttpClientHandler
                {
                    CookieContainer = sp.GetRequiredService<ICookieContainerAccessor>()
                                        .CookieContainer
                });

            services.AddTransient<IAirportService, AirportService>();
            services.AddTransient<IAirlineService, AirlineService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/App/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=App}/{action=DisplaySystemDetails}/{id?}");
                });
        }
    }
}
