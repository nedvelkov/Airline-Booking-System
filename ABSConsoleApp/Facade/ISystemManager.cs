namespace Facade
{
    public interface ISystemManager
    {
        public string CreateAirport(string name);
       
        public string CreateAirline(string name);
        
        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);
        
        public string CreateSection(string airlineName, string flightId, int rows, int colms, int seatClass);

        public string FindAvailableFlights(string origin, string destination);

        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char colmn);

        public string DisplaySystemDetails();
    }
}
