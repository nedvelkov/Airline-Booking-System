using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;

namespace ABS_WebAPI.Services.Models
{
    public class SeatService : ISeatService
    {
        private readonly ISystemManager _manager;

        public SeatService(ISystemManager manager) => _manager = manager;
        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
            => _manager.BookSeat(airlineName, flightId, seatClass, row, column);
    }
}
