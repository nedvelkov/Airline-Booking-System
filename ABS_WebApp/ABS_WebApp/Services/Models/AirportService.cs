using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.RequestModels;
using Microsoft.Extensions.Caching.Memory;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp.Services.Models
{
    public class AirportService: IAirportService
    {
        private readonly WebApiService _webApiService;
        private readonly IMemoryCache _cache;

        public AirportService(WebApiService webApiService, IMemoryCache cache)
        {
            _webApiService = webApiService;
            _cache = cache;
        }

        public async Task<string> CreateAirport(string name)
        {
            var airport = new AirportRequestModel { Name = name };
            return await _webApiService.CreateAirport(airport);
        }

        public async Task< IReadOnlyList<string>> Airports()
        {
            IEnumerable<string> result;

            if (!_cache.TryGetValue(nameof(Airports), out result))
            {
                var data = await _webApiService.GetAirports();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(CACHE_EXPIRATION_IN_SECONDS));
                _cache.Set(nameof(Airports), data, cacheEntryOptions);
                return data.ToList();
            }

            return result.ToList();
        }

    }
}
