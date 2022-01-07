using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ABS_SystemManager;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using ABS_WebAPI.Services.Models;
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
            services.AddTransient<IAirportService, AirportService>();
            services.AddTransient<IAirlineService, AirlineService>();
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<ISeatService, SeatService>();
            services.AddTransient<ISystemService, SystemService>();
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

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
