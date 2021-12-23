using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_WebApp.Services.Interfaces;
using ABS_Models;


namespace ABS_WebApp.Services.Models
{
    public class FlightService : IFlightService
    {
        private readonly WebApiService _webApiService;

        public FlightService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> BookSeat(BookSeatModel seat) => await _webApiService.BookSeat(seat);

        public async Task<string> CreateFlight(FlightModel flight) => await _webApiService.CreateFlight(flight);

        public async Task<string> CreateFlightSection(FlightSectionModel flightSection) => await _webApiService.CreateSection(flightSection);

        public async Task<string> FindAvailableFlights(AviableFlightsModel flight) => await _webApiService.GetAviableFlights(flight);

        public async Task<IReadOnlyList<string>> Flights()
        {
            IEnumerable<string> result = await _webApiService.GetFlights();
            return result.ToList();
        }

    }
}
