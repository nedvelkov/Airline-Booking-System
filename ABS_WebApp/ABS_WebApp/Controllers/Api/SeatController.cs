using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ABS_WebApp.Services.Interfaces;
using ABS_WebApp.ViewModels;

namespace ABS_WebApp.Controllers.Api
{
    [Route("/api/airline/{airlinename}/flight/{flightid}/section/{seatsection}/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public SeatController(IFlightService flightService)
            => _flightService = flightService;

        [HttpPut]
        public async Task<ActionResult<string>> Put(BookSeatViewModel model)
        {
            return await _flightService.BookSeat(model.AirlineName, model.Id, model.SeatClass, model.Row, model.Column[0]);
        }
    }
}
