using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.Services.RequestModels;
using Microsoft.Extensions.Caching.Memory;
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

        public async Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
        {
            var seat = new BookSeatRequestModel() { AirlineName = airlineName, Id = flightId, SeatClass = seatClass, Row = row, Column = column };
            return await _webApiService.BookSeat(seat);
        }

        public async Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            var flight = new FlightRequestModel() { AirlineName = airlineName, Origin = origin, Destination = destination, DateOfFlight = new DateTime(year, month, day), Id = id };
            return await _webApiService.CreateFlight(flight);
        }

        public async Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass)
        {
            var section = new SectionRequestModel() { AirlineName = airlineName, Id = flightId, Rows = rows, Columns = columns, SeatClass = seatClass };
            return await _webApiService.CreateSection(section);
        }

        public async Task<string> FindAvailableFlights(string origin, string destination)
        {
            var requestModel = new FindFlightRequestModel() { Origin = origin, Destination = destination };
            return await _webApiService.GetAviableFlights(requestModel);
        }

        public async Task<IReadOnlyList<string>> Flights()
        {

            IEnumerable<string> result;

            if (!_cache.TryGetValue(nameof(Flights), out result))
            {
                var data = await _webApiService.GetFlights();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(expirationSeconds));
                _cache.Set(nameof(Flights), data, cacheEntryOptions);
                return data.ToList();
            }

            return result.ToList();
        }

    }
}
