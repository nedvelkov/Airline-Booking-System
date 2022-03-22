using ABS_SystemManager.Data.UserDefineModels;
using ABS_SystemManager.Data.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using ABS_Models;

namespace ABS_SystemManager.Interfaces
{
    public interface IAbsRepository
    {
        List<string> ListAirlines { get; }
        List<string> ListAirports { get; }
        List<string> ListFlights { get; }

        Task<bool> BookSeat(string airlineName, string flightId, int seatClass, int row, char column);
        Task<bool> CreateAirline(string name);
        Task<bool> CreateAirport(string name);
        Task<bool> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);
        Task<bool> CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass);
        Task<List<FlightsModel>> FindAvailableFlights(string origin, string destination);
        Task<List<FlightsModel>> GetFlightsByAirlineName(string airlineName);
        Task<List<FlightSectionTableView>> GetFlightSectionsForFlight(string flightId);
        Task<List<AirlineViewModel>> GetAirlineViews();
        Task<List<AirlineSystemDisplay>> GetAirlineWithFlightsViews();
        Task<SeatNumber> GetLastSeatNumber(string flightId, int seatClass);
        Task<bool> HasAirport(string name);
    }
}