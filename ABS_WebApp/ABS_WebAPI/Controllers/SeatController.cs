using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebAPI.Controllers
{
    [Route(SEAT_API_PATH)]
    [ApiController]
    public class SeatController:ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService) => _seatService = seatService;

        [HttpPut]
        public string Put(BookSeatModel seat) 
            => _seatService.BookSeat(seat.AirlineName, seat.Id, seat.SeatClass, seat.Row, seat.Column);
    }
}
