using Microsoft.AspNetCore.Mvc;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebAPI.Controllers
{
    [Route(seatApi)]
    [ApiController]
    public class SeatController:ControllerBase
    {
        private readonly ISeatService _seatService;

        public SeatController(ISeatService seatService) => _seatService = seatService;

        [HttpPut]
        public string Put(BookSeatRequestModel seat) 
            => _seatService.BookSeat(seat.AirlineName, seat.Id, seat.SeatClass, seat.Row, seat.Column);
    }
}
