using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ABS_WebApp.Services;
using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.Models;

using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<WebApiService>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = COOKIE_SHEME_NAME;
            }).AddCookie(COOKIE_SHEME_NAME, opt =>
            {
                opt.Cookie.Name = COOKIE_TOKEN_NAME;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
                opt.LoginPath = "/account/login";
            });
            services.AddSingleton<IAccountService, AccountService>();
            services.AddControllersWithViews();
        }

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
               .UseAuthorization();

            app.ApplicationServices.GetService<IAccountService>().SeedAdmin();

            app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "Admin",
                     pattern: "{area:exists}/{controller=App}/{action=DisplaySystemDetails}/{id?}");
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=App}/{action=DisplaySystemDetails}/{id?}");
             });
        }
    }
}
