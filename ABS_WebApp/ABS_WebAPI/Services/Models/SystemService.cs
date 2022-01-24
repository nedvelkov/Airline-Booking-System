using ABS_WebApp.Seeder;
using ABS_WebAPI.Services.Interfaces;
using ABS_SystemManager.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class SystemService : ISystemService
    {
        private readonly ISystemManager _manager;

        public SystemService(ISystemManager manager) => _manager = manager;
        public string Details() => _manager.DisplaySystemDetails();
        public void SeedData()
        {
            var seeder = new DataSeeder();
            seeder.Seed(_manager);
        }
    }
}
