using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_SystemManager.Interfaces;
using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class AirportService: IAirportService
    {
        private readonly ISystemManager _manager;

        public AirportService(ISystemManager manager) => _manager = manager;

        public Task<string> CreateAirport(string name) 
            => Task.FromResult(_manager.CreateAirport(name));

        public IReadOnlyList<string> Airports => _manager.ListAirports;

    }
}
