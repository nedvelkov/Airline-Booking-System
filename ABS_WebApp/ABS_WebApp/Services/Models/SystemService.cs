using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class SystemService : ISystemService
    {
        public Task<string> Details()
            => Task.FromResult(_manager.DisplaySystemDetails());
    }
}
