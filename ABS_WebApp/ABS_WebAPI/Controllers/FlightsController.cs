using System.Collections.Generic;
using System.Threading.Tasks;
using ABS_Models;
using ABS_SystemManager.Data.UserDefineModels;
using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ABS_DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.SystemError;

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FlightsModel>>> GetAviableFlights(string origin, string destination)
        {
            var result = await _flightService.FindAvailableFlights(origin, destination);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else if (result.Count == 0)
            {
                return NoContent();
            }

            return result;
        }

        [HttpGet]
        [Route(FIND_FLIGHT_BY_NAME_API_PATH)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<FlightsModel>> GetFlightsByAirlineName(string airlineName)
        {
            return await _flightService.GetFlightsByAirlineName(airlineName);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post(FlightModel flight)
        {
            var result = await _flightService.CreateFlight(flight.AirlineName, flight.Origin, flight.Destination, flight.DateOfFlight.Year, flight.DateOfFlight.Month, flight.DateOfFlight.Day, flight.Id);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return Ok(result);
            }
            return UnprocessableEntity(result);
        }

    }
}
