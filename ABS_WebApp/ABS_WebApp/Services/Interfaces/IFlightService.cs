using System.Threading.Tasks;
using System.Collections.Generic;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IFlightService
    {
        public Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);

        public Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass);

        public Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column);

        public Task<string> FindAvailableFlights(string origin, string destination);

        public Task<IReadOnlyList<string>> Flights();
    }
}
