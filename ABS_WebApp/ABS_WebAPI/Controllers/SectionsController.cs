using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebAPI.Controllers
{
    [Route(SECTION_API_PATH)]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService) => _sectionService = sectionService;

        [HttpPost]
        public ActionResult<string> Post(FlightSectionModel section)
        {
            var result = _sectionService.CreateFlightSection(section.AirlineName, section.Id, section.Rows, section.Columns, section.SeatClass);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }
    }
}
