using System.Collections.Generic;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface IAirlineService
    {
        public string CreateAirline(string name);

        public IEnumerable<string> GetAirlines();
    }
}
