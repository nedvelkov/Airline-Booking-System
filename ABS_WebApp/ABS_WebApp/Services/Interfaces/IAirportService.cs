using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_Models;

namespace ABS_WebApp.Services.Interfaces
{
   public interface IAirportService
    {
        public Task<string> CreateAirport(AirportModel airport);

        public Task<IReadOnlyList<string>> Airports();
    }
}
