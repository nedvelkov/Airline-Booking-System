namespace Facade
{
    using Models.Enums;
    public interface ISystemManager
    {
        public void CreateAirport(string name);
       
        public void CreateAirline(string name);
        
        public void CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);
        
        public void CreateSection(string airlineName, string flightId, int rows, int colms, SeatClass seatClass);

        public string FindAvailableFlights(string origin, string destination);

        public void BookSeat(string airlineName, string flightId, SeatClass seatClass, int row, char colmn);

        public void DisplaySystemDetails();
    }
}
