using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.RequestModels;

namespace ABS_WebApp.Services.Models
{
    public class AirlineService : IAirlineService
    {
        private readonly WebApiService _webApiService;

        public AirlineService(WebApiService webApiService) => _webApiService = webApiService;

        public async Task<string> CreateAirline(string name)
        {
            var airline = new AirlaneRequestModel() { Name = name };
            return await _webApiService.CreateAirline(airline);
        }

        public async Task<IReadOnlyList<string>> Airlines()
        {
            var result = await _webApiService.GetAirlines();
            return result.ToList();
        }
    }
}
