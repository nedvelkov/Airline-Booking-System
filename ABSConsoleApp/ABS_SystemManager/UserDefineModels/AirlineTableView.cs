namespace ABS_SystemManager.UserDefineModels
{
    public class AirlineTableView
    {
        public string AirlineName { get; set; }
        public string FlightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public short? SeatClass { get; set; }
        public short? Row { get; set; }
        public string Column { get; set; }
        public bool? Booked { get; set; }
    }
}
