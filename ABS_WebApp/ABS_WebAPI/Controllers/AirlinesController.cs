using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Http;

namespace ABS_WebAPI.Controllers
{
    [Route(AIRLINE_API_PATH)]
    [ApiController]
    public class AirlinesController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlinesController(IAirlineService airlineService)
            => _airlineService = airlineService;

        [HttpGet]
        [ResponseCache(Duration = SHARED_CACHE_EXPIRATION_IN_SECONDS)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> Get() => _airlineService.GetAirlines();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult Post(AirlineModel airline)
        {
            var result = _airlineService.CreateAirline(airline.Name);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return Ok(result);
            }
            return UnprocessableEntity(result);
        }

    }
}
