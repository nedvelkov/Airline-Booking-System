using ABS_SystemManager.Data.UserDefineModels;
using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Models
{
    public class SectionService : ISectionService
    {
        private readonly ISystemManager _manager;

        public SectionService(ISystemManager manager) => _manager = manager;

        public async Task< string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass)
            => await _manager.CreateSection(airlineName, flightId, rows, columns, seatClass);
        public async Task<List<FlightSectionView>> GetFlightSectionsForFlight(string flightId) => await _manager.GetFlightSectionsForFlight(flightId);
    }
}
