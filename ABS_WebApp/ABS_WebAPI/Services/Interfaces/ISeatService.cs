using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISeatService
    {
        public Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column);
    }
}
