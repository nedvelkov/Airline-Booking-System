using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface IAirportService
    {
        public Task<string> CreateAirport(string name);

        public IEnumerable<string> GetAiports();
    }
}
