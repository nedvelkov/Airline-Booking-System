namespace Facade.DataConstants
{
    public static class Error
    {
        //Common
        public const string dublicateName = "{0} with this {1} already exist";


        //Airport
        public const string airportName = "Airport name must be 3 upper letters";
        public const string airportMissing = "Airport with name {0} don't exist";

        //Airlane
        public const string airlineName = "Name of airline must have only letters and length between 1 and 5 characters";
        public const string airlineMissing = "Airline with name {0} don't exist";

        //Flight
        public const string wrongDestination = "Destionation must be different from origin";

        //SeatClass
        public const string invalidSeatClass = "Seat class is not valid";
        public const string invalidSeatCount = "{0} of seat must be between {1} and {2}";

        //Seat
        public const string wrongSeatNumber = "Seat with this number doesn't exist";
        public const string bookedSeat = "This seat is already booked";

        //SeatNumber
        public const string invalidSeatRow = "Invalid seat row";
        public const string invalidSeatColmn = "Invalid seat column";

        //Date
        public const string notValidDate = "Date is not valid";
    }
}
