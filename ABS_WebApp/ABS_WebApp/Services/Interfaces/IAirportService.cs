using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebApp.Services.Interfaces
{
   public interface IAirportService
    {
        public Task<string> CreateAirport(string name);

        public IReadOnlyList<string> Airports { get; }
    }
}
