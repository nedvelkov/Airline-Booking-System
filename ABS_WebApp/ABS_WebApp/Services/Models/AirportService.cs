using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_Models;

namespace ABS_WebApp.Services.Models
{
    public class AirportService : IAirportService
    {
        private readonly WebApiService _webApiService;

        public AirportService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> CreateAirport(AirportModel airport) => await _webApiService.CreateAirport(airport);

        public async Task<IReadOnlyList<string>> Airports()
        {
            IEnumerable<string> result = await _webApiService.GetAirports();
            return result.ToList();
        }
    }
}
