using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Http;

namespace ABS_WebAPI.Controllers
{
    [Route(SECTION_API_PATH)]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService) => _sectionService = sectionService;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public IActionResult Post(FlightSectionModel section)
        {
            var result = _sectionService.CreateFlightSection(section.AirlineName, section.Id, section.Rows, section.Columns, section.SeatClass);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return Ok(result);
            }
            return UnprocessableEntity(result);
        }
    }
}
