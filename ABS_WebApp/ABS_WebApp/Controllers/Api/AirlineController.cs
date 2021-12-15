using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(IAirlineService airlineService)
            => _airlineService = airlineService;


        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var list = await _airlineService.Airlines;
            return list.ToList();
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<string>> Get(string name)
        {
            var list = await _airlineService.Airlines;
            var airline = list.FirstOrDefault(x => x.Contains(name));
            if (airline == null)
            {
                return NotFound();
            }
            return airline;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(CreateAirlineViewModel model)
        {
            var list = await _airlineService.Airlines;
            var airline = list.FirstOrDefault(x => x.Contains(model.Name));
            if (airline != null)
            {
                return BadRequest("Airline with this name already exist");
            }
            return await _airlineService.CreateAirline(model.Name);
        }

    }
}
