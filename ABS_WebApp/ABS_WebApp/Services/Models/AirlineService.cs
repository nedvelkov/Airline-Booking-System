using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_SystemManager.Interfaces;
using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class AirlineService : IAirlineService
    {
        private readonly ISystemManager _manager;

        public AirlineService(ISystemManager manager) => _manager = manager;

        public Task<string> CreateAirline(string name)
            => Task.FromResult(_manager.CreateAirline(name));

        public Task< IReadOnlyList<string>> Airlines => Task.FromResult(_manager.ListAirlines);

    }
}
