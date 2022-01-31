using System.Collections.Generic;
using System.Threading.Tasks;
using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebAPI.Controllers
{
    [Route(FLIGHT_API_PATH)]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService) => _flightService = flightService;

        [HttpGet]
        [ResponseCache(Duration = SHARED_CACHE_EXPIRATION_IN_SECONDS)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> Get() => _flightService.GetFlights();

        [HttpGet]
        [Route(FIND_FLIGHT_API_PATH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> GetAviableFlights(string origin, string destination)
        {
            return await _flightService.FindAvailableFlights(origin, destination);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post(FlightModel flight)
        {
            var result =await _flightService.CreateFlight(flight.AirlineName, flight.Origin, flight.Destination, flight.DateOfFlight.Year, flight.DateOfFlight.Month, flight.DateOfFlight.Day, flight.Id);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return Ok(result);
            }
            return UnprocessableEntity(result);
        }

    }
}
