using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_Models;

namespace ABS_WebApp.Services.Interfaces
{
    public interface IAirlineService
    {
        public Task<string> CreateAirline(AirlineModel airline);

        public Task<IReadOnlyList<string>> Airlines();
    }
}
