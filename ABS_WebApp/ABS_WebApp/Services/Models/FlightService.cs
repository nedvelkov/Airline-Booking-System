using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Services.Models
{
    public class FlightService : IFlightService
    {
        private readonly WebApiService _webApiService;

        public FlightService(WebApiService webApiService) => _webApiService = webApiService;

        public Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
                       => Task.FromResult("OK");

        public Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
                        => Task.FromResult("OK");

        public Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass)
            => Task.FromResult("OK");

        public Task<string> FindAvailableFlights(string origin, string destination)
            => Task.FromResult("OK");

       // public Task<IReadOnlyList<string>> Flights => Task.FromResult(new List<string> { "OK","OK" });

    }
}
