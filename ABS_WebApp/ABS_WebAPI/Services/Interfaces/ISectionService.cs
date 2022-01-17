using System.Threading.Tasks;

namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISectionService
    {
        public Task<string> CreateFlightSection(string airlineName, string flightId, int rows, int columns, int seatClass);
    }
}
