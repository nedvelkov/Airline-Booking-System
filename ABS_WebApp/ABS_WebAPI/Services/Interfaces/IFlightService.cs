using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
   public interface IFlightService
    {
        public Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);

        public string FindAvailableFlights(string origin, string destination);

        public IEnumerable<string> GetFlights();
    }
}
