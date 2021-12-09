using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_SystemManager.Interfaces;
using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class FlightService : IFlightService
    {
        private readonly ISystemManager _manager;

        public FlightService(ISystemManager manager) => _manager = manager;

        public Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
            => Task.FromResult(_manager.BookSeat(airlineName, flightId, seatClass, row, column));

        public Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
            => Task.FromResult(_manager.CreateFlight(airlineName, origin, destination, year, month, day, id));

        public Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass)
            => Task.FromResult(_manager.CreateSection(airlineName, flightId, rows, columns, seatClass));

        public Task<string> FindAvailableFlights(string origin, string destination)
            => Task.FromResult(_manager.FindAvailableFlights(origin, destination));

        public Task<IReadOnlyList<string>> Flights => Task.FromResult(_manager.ListFlights);

    }
}
