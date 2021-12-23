using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_Models;

namespace ABS_WebApp.Services.Models
{
    public class AirlineService : IAirlineService
    {
        private readonly WebApiService _webApiService;

        public AirlineService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> CreateAirline(AirlineModel airline) => await _webApiService.CreateAirline(airline);

        public async Task<IReadOnlyList<string>> Airlines()
        {
            IEnumerable<string> result= await _webApiService.GetAirlines();
            return result.ToList();
        }
    }
}
