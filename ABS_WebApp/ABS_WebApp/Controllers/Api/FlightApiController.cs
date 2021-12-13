using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ABS_WebApp.Services.Interfaces;

namespace ABS_WebApp.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FlightApiController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightApiController(IFlightService flightService)
            => this._flightService = flightService;

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var list = await _flightService.Flights;
            return list.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(id));
            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<string>> Post(string id)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(id));
            if (flight == null)
            {
                return NotFound();
            }

            return "Found!";
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(string id)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(id));
            if (flight == null)
            {
                return NotFound();
            }

            return "Deleted";
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<string>> Patch(string id)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(id));
            if (flight == null)
            {
                return NotFound();
            }

            return "Patched";
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(string id)
        {
            var list = await _flightService.Flights;
            var flight = list.FirstOrDefault(x => x.Contains(id));
            if (flight == null)
            {
                return NotFound();
            }

            return "Updated";
        }
    }
}
