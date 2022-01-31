using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Models
{
    public class FlightService : IFlightService
    {
        private readonly ISystemManager _manager;

        public FlightService(ISystemManager manager) => _manager = manager;

        public IEnumerable<string> GetFlights() => _manager.ListFlights;

        public async Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
            => await _manager.CreateFlight(airlineName, origin, destination, year, month, day, id);

        public async Task<string> FindAvailableFlights(string origin, string destination)
            => await _manager.FindAvailableFlights(origin, destination);
    }
}
