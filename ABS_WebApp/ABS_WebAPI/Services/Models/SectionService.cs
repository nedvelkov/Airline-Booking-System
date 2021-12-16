using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class SectionService : ISectionService
    {
        private readonly ISystemManager _manager;

        public SectionService(ISystemManager manager) => _manager = manager;

        public string CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass)
            => _manager.CreateSection(airlineName, flightId, rows, columns, seatClass);
    }
}
