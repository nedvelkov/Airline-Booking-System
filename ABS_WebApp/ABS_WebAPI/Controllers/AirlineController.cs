using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
            => _airlineService = airlineService;

        [HttpGet]
        public IEnumerable<string> Get() => _airlineService.GetAirlines();

        [HttpPost]

        public string Post(AirlineRequestModel airline) => _airlineService.CreateAirline(airline.Name);

    }
}
