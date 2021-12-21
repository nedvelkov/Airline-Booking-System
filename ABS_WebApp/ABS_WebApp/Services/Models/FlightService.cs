using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_WebApp.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using ABS_Models;

using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp.Services.Models
{
    public class FlightService : IFlightService
    {
        private readonly WebApiService _webApiService;
        private readonly IMemoryCache _cache;

        public FlightService(WebApiService webApiService, IMemoryCache cache)
        {
            _webApiService = webApiService;
            _cache = cache;
        }

        public async Task<string> BookSeat(BookSeatModel seat) => await _webApiService.BookSeat(seat);

        public async Task<string> CreateFlight(FlightModel flight) => await _webApiService.CreateFlight(flight);

        public async Task<string> CreateFlightSection(FlightSectionModel flightSection) => await _webApiService.CreateSection(flightSection);

        public async Task<string> FindAvailableFlights(AviableFlightsModel flight) => await _webApiService.GetAviableFlights(flight);

        public async Task<IReadOnlyList<string>> Flights()
        {

            IEnumerable<string> result;

            if (!_cache.TryGetValue(nameof(Flights), out result))
            {
                var data = await _webApiService.GetFlights();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(CACHE_EXPIRATION_IN_SECONDS));
                _cache.Set(nameof(Flights), data, cacheEntryOptions);
                return data.ToList();
            }

            return result.ToList();
        }

    }
}
