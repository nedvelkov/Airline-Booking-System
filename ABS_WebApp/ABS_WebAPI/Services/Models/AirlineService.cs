using System.Collections.Generic;
using System.Threading.Tasks;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class AirlineService : IAirlineService
    {
        private readonly ISystemManager _manager;

        public AirlineService(ISystemManager manager) => _manager = manager;

        public async Task<string> CreateAirline(string name) => await _manager.CreateAirline(name);

        public IEnumerable<string> GetAirlines() => _manager.ListAirlines;
    }
}
