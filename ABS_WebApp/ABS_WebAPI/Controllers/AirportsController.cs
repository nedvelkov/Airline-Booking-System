﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Http;

namespace ABS_WebAPI.Controllers
{
    [Route(AIRPORT_API_PATH)]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportsController(IAirportService airportService) => _airportService = airportService;

        [HttpGet]
        [ResponseCache(Duration = SHARED_CACHE_EXPIRATION_IN_SECONDS)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<string> Get() => _airportService.GetAiports();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<string> Post(AirportModel airport)
        {
            var result= _airportService.CreateAirport(airport.Name);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }
    }
}
