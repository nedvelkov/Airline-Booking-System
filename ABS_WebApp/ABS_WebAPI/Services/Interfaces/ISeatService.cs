namespace ABS_WebAPI.Services.Interfaces
{
    public interface ISeatService
    {
        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char column);
    }
}
