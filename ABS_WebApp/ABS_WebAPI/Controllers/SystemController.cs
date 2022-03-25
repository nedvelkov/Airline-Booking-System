using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Authorization;

namespace ABS_WebAPI.Controllers
{
    [Route(SUSTEM_API_PATH)]
    [ApiController]

    public class SystemController : ControllerBase
    {
        private readonly ISystemService _system;

        public SystemController(ISystemService system) => _system = system;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = USER_ADMIN_ROLE)]
        public async Task<ActionResult<SystemDetailsModel>> Get()
        {
            var details = await _system.Details();
            if (details.AirlineList.Count == 0 && details.AirportList.Count == 0)
            {
                return NoContent();
            }
            return details;
        }
    }
}
