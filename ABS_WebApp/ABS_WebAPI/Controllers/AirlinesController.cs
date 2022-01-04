using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;

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
        public IEnumerable<string> Get() => _airlineService.GetAirlines();

        [HttpPost]
        public ActionResult<string> Post(AirlineModel airline)
        {
            var result = _airlineService.CreateAirline(airline.Name);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }

    }
}
