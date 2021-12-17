using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.RequestModels;

namespace ABS_WebApp.Services.Models
{
    public class AirportService: IAirportService
    {
        private readonly WebApiService _webApiService;

        public AirportService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> CreateAirport(string name)
        {
            var airport = new AirportRequestModel { Name = name };
            return await _webApiService.CreateAirport(airport);
        }

        public async Task< IReadOnlyList<string>> Airports()
        {
            var result = await _webApiService.GetAirports();
            return result.ToList();
        }

    }
}
