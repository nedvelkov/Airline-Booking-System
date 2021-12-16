using System.Collections.Generic;

namespace ABS_SystemManager.Interfaces
{
    public interface ISystemManager
    {
        public string CreateAirport(string name);

        public string CreateAirline(string name);

        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);

        public string CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass);

        public string FindAvailableFlights(string origin, string destination);

        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char column);

        public string DisplaySystemDetails();

        public IReadOnlyList<string> ListAirlines { get; }
        public IReadOnlyList<string> ListAirports { get; }
        public IReadOnlyList<string> ListFlights { get; }
    }
}
