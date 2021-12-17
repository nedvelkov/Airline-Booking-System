namespace ABS_WebApp.Services.RequestModels
{
    public class BookSeatRequestModel
    {
        public string Id { get; set; }

        public string AirlineName { get; set; }

        public int Row { get; set; }

        public char Column { get; set; }

        public int SeatClass { get; set; }
    }
}
