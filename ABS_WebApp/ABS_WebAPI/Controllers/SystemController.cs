using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SystemController:ControllerBase
    {
        private readonly ISystemService _system;

        public SystemController(ISystemService system) => _system = system;

        [HttpGet]
        public string Get()
        {
            _system.SeedData();
            return _system.Details();
        }
    }
}
