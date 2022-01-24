using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_SystemManager.Interfaces
{
    public interface ISystemManager
    {
        public Task<string> CreateAirport(string name);

        public Task<bool> HasAirport(string name);

        public Task<string> CreateAirline(string name);

        public Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);

        public Task<string> CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass);

        public string FindAvailableFlights(string origin, string destination);

        public Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column);

        public string DisplaySystemDetails();

        public IReadOnlyList<string> ListAirlines { get; }

        public IReadOnlyList<string> ListAirports { get; }

        public IReadOnlyList<string> ListFlights { get; }
    }
}
