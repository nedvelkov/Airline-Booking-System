using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Controllers.Api
{
    [Route("/api/airline/{airlinename}/flight/{flightid}/[controller]")]
    [ApiController]
    public class SectionController:ControllerBase
    {
        private readonly IFlightService _flightService;

        public SectionController(IFlightService flightService) 
            => _flightService = flightService;

        [HttpPost]
        public async Task<ActionResult<string>> Post(CreateSectionViewModel model)
        {
            return await _flightService.CreateFlightSection(model.AirlineName, model.Id, model.Rows, model.Columns, model.SeatClass);
        }
    }
}
