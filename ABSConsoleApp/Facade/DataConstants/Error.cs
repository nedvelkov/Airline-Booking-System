namespace Facade.DataConstants
{
    public static class Error
    {
        //Common
        public const string dublicateItem = "{0} with this {1} already exist";
        public const string missingItem = "{0} with {1} {2} don't exist";


        //Airport
        public const string airportName = "Airport name must be 3 upper letters";

        //Airlane
        public const string airlineName = "Name of airline must have only letters and length between 1 and 5 characters";
        public const string invalidFlightId = "Airline {0} don't have flight with id {1}";

        //Flight
        public const string flightId = "Flight id must be with numbers and letters only";
        public const string wrongDestination = "Destionation must be different from origin";
        public const string noFlights = "There is no flight from {0} to {1}, at this time";
        public const string departedFlight = "Flight is already departed";

        //SeatClass
        public const string invalidSeatClass = "Seat class is not valid";
        public const string invalidSeatCount = "{0} of seat must be between {1} and {2}";

        //Seat
        public const string wrongSeatNumber = "Seat with this number doesn't exist";
        public const string bookedSeat = "This seat is already booked";
        public const string invalidCount = "{0} of seat must be between {1} and {2}";

        //SeatNumber
        public const string invalidSeatRow = "Invalid seat row";
        public const string invalidSeatColmn = "Invalid seat column";

        //Date
        public const string notValidDate = "Date is not valid";
        public const string notValidFlightDate = "Enter valid date for flight departure";
    }
}
