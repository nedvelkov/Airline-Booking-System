using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.RequestModels;
using Microsoft.Extensions.Caching.Memory;
using static ABS_DataConstants.DataConstrain;


namespace ABS_WebApp.Services.Models
{
    public class AirlineService : IAirlineService
    {
        private readonly WebApiService _webApiService;
        private readonly IMemoryCache _cache;

        public AirlineService(WebApiService webApiService, IMemoryCache cache)
        {
            _webApiService = webApiService;
            _cache = cache;
        }

        public async Task<string> CreateAirline(string name)
        {
            var airline = new AirlaneRequestModel() { Name = name };
            return await _webApiService.CreateAirline(airline);
        }

        public async Task<IReadOnlyList<string>> Airlines()
        {
            IEnumerable<string> result;

            if (!_cache.TryGetValue(nameof(Airlines), out result))
            {
                var data = await _webApiService.GetAirlines();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(CACHE_EXPIRATION_IN_SECONDS));
                _cache.Set(nameof(Airlines), data, cacheEntryOptions);
                return data.ToList();
            }

            return result.ToList();
        }
    }
}
