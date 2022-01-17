using System.Collections.Generic;
using System.Threading.Tasks;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class AirportService : IAirportService
    {
        private readonly ISystemManager _manager;

        public AirportService(ISystemManager manager) => _manager = manager;

        public async Task<string> CreateAirport(string name) =>await _manager.CreateAirport(name);

        public IEnumerable<string> GetAiports() => _manager.ListAirports;
    }
}
