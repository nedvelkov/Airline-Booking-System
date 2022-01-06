using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

using ABS_SystemManager;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using ABS_WebAPI.Services.Models;

using static ABS_DataConstants.DataConstrain;
using ABS_WebAPI.Middleware;

namespace ABS_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            services.AddSingleton<ISystemManager, SystemManager>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAirportService, AirportService>();
            services.AddTransient<IAirlineService, AirlineService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = COOKIE_SHEME_NAME;
            }).AddCookie(COOKIE_SHEME_NAME, opt =>
            {
                opt.Cookie.Name = COOKIE_TOKEN_NAME;
                opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                opt.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddCors();
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;

            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseResponseCaching();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
