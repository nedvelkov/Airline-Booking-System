using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AirportController:ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService) => _airportService = airportService;

        [HttpGet]
        public IEnumerable<string> Get() => _airportService.GetAiports();

        [HttpPost]
        public string Post(AirportRequestModel airport) => _airportService.CreateAirport(airport.Name);
    }
}
