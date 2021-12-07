namespace ABS_SystemManager.DataConstants
{
    public static class Success
    {
        //Airport
        public const string createdAirport = "Airport {0} is created successfully";

        //Airline
        public const string createdAirline = "Airline {0} is created successfully";

        //Flight
        public const string createFlight = "Flight from {0} to {1} on airline {2} is created successfully";
        public const string testFlightToDestination= "Flight #{0} from {1} to {2}.Departure at {3}\r\nThe flight has {4} section.\r\n";

        //FlightSection
        public const string createFlightSection = "Section {0} class is created successfully for flight from {1} to {2} on airline {3}";

        //Seat
        public const string bookedSeat = "Seat {0} in {1} class is successfully booked for flight from {2} to {3} on airline {4}";

    }
}
