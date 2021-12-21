using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_Models;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IFlightService
    {
        public Task<string> CreateFlight(FlightModel flight);

        public Task<string> CreateFlightSection(FlightSectionModel flightSection);

        public Task<string> BookSeat(BookSeatModel seat);

        public Task<string> FindAvailableFlights(AviableFlightsModel flight);

        public Task<IReadOnlyList<string>> Flights();
    }
}
