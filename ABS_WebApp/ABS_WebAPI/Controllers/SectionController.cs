using Microsoft.AspNetCore.Mvc;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/{airlinename}/flight/{flightid}/[controller]")]
    [ApiController]
    public class SectionController:ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService) => _sectionService = sectionService;

        [HttpPost("seatclass:int")]
        public string Post(string airlineName, string flightId, int seatClass, SectionRequestModel section)
            => _sectionService.CreateFlightSection(airlineName, flightId, section.Rows, section.Columns, seatClass);
    }
}
