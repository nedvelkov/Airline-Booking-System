using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ABS_SystemManager;
using ABS_SystemManager.Data;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using ABS_WebAPI.Services.Models;
using ABS_WebAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            services.AddDbContext<ABS_databaseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAbsRepository, AbsRepository>();
            services.AddResponseCaching();
            services.AddTransient<ISystemManager, SystemManager>();
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
            services.AddCors();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://abs-domain.us.auth0.com/";
                options.Audience = "http://localhost:1618/api";
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

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.ApplicationServices.CreateScope().ServiceProvider.GetService<ISystemService>().SeedData();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
