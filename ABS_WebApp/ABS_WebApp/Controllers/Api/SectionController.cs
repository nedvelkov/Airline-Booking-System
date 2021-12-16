using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = await _flightService.CreateFlightSection(model.AirlineName, model.Id, model.Rows, model.Columns, model.SeatClass);
            return result;
        }
    }
}
