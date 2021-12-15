using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Controllers.Api
{
    [Route("/api/airline/{airlinename}/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
            => this._flightService = flightService;

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var list = await _flightService.Flights;
            return list.ToList();
        }

        [HttpGet("{flightid}")]
        public async Task<ActionResult<string>> Get(string flightid)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(flightid));
            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(CreateFlightViewModel model)
        {
            return Ok();
        }

    }
}
