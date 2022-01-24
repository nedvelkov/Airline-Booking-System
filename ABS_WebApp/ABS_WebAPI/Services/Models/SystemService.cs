using ABS_WebApp.Seeder;
using ABS_WebAPI.Services.Interfaces;
using ABS_SystemManager.Interfaces;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Models
{
    public class SystemService : ISystemService
    {
        private readonly ISystemManager _manager;

        public SystemService(ISystemManager manager) => _manager = manager;
        public string Details() => _manager.DisplaySystemDetails();
        public async Task<bool> SeedData()
        {
            var seeder = new DataSeeder();
            return await seeder.Seed(_manager);
        }
    }
}
