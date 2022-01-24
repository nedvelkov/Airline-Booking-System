using ABS_SystemManager.Interfaces;
using ABS_WebAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Models
{
    public class SeatService : ISeatService
    {
        private readonly ISystemManager _manager;

        public SeatService(ISystemManager manager) => _manager = manager;
        public async Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
            => await  _manager.BookSeat(airlineName, flightId, seatClass, row, column);
    }
}
