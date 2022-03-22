using System.Threading.Tasks;
using System.Collections.Generic;
using ABS_SystemManager.Data.UserDefineModels;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISectionService
    {
        public Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass);
        public Task<List<FlightSectionView>> GetFlightSectionsForFlight(string flightId);
    }
}
