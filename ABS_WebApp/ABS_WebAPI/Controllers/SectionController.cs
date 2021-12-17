using Microsoft.AspNetCore.Mvc;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService) => _sectionService = sectionService;

        [HttpPost]
        public string Post(SectionRequestModel section)
            => _sectionService.CreateFlightSection(section.AirlineName, section.Id, section.Rows, section.Columns, section.SeatClass);
    }
}
