using System.Collections.Generic;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface IAirportService
    {
        public string CreateAirport(string name);

        public IEnumerable<string> GetAiports();
    }
}
