using Microsoft.AspNetCore.Mvc;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ABS_WebAPI.Controllers
{
    [Route(SUSTEM_API_PATH)]
    [ApiController]
    public class SystemController:ControllerBase
    {
        private readonly ISystemService _system;

        public SystemController(ISystemService system) => _system = system;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<string> Get() => await _system.Details();
    }
}
