using System;
using System.Threading.Tasks;

using ABS_WebApp.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp.Services.Models
{
    public class SystemService : ISystemService
    {
        private readonly WebApiService _webApiService;
        private readonly IMemoryCache _cache;

        public SystemService(WebApiService webApiService, IMemoryCache cache)
        {
            _webApiService = webApiService;
            _cache = cache;
        }

        public async Task<string> Details()
        {
            string result;

            if (!_cache.TryGetValue(nameof(Details), out result))
            {
                var data = await _webApiService.GetSystemDetails();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expirationSeconds));
                _cache.Set(nameof(Details), data, cacheEntryOptions);
                return data;
            }
            return result;

        }
    }
}
