using System.Threading.Tasks;

using ABS_SystemManager.Interfaces;
using ABS_WebApp.Seeder;
using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class SystemService : ISystemService
    {
        private readonly ISystemManager _manager;

        public SystemService(ISystemManager manager) => _manager = manager;

        public Task<string> Details()
            => Task.FromResult(_manager.DisplaySystemDetails());

        public void SeedData()
        {
            DataSeeder.Seed(_manager);
        }
    }
}
