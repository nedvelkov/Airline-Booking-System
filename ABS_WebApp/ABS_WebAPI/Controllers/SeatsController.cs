using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;
using Microsoft.AspNetCore.Http;

namespace ABS_WebAPI.Controllers
{
    [Route(SEAT_API_PATH)]
    [ApiController]
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService) => _seatService = seatService;

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult<string> Put(BookSeatModel seat)
        {
            var result = _seatService.BookSeat(seat.AirlineName, seat.Id, seat.SeatClass, seat.Row, seat.Column);
            if (result.Contains(SUCCESSFULL_OPERATION))
            {
                return result;
            }
            return UnprocessableEntity(result);
        }
    }
}
