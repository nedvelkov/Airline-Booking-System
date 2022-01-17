using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface IAirlineService
    {
        public Task<string> CreateAirline(string name);

        public IEnumerable<string> GetAirlines();
    }
}
