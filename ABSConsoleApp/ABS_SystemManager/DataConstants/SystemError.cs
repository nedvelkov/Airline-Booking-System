namespace ABS_SystemManager.DataConstants
{
    public static class SystemError
    {
        //Airlane
        public const string MISSING_FLIGHT_FROM_AIRLINE = "Airline {0} don't have flight with id {1}";

        //Flight
        public const string WRONG_DESTINATION = "Destionation must be different from origin";
        public const string NO_AVIABLE_FLIGHTS = "There is no flight from {0} to {1}, at this time";
        public const string DEPARTED_FLIGHT = "Flight is already departed";

        //SeatClass
        public const string INVALID_SEAT_COUNT = "{0} of seat must be between {1} and {2}";

        //Seat
        public const string BOOEKD_SEAT = "This seat is already booked";

        //Date
        public const string INVALID_DATE_OF_DEPARTURE_FLIGHT = "Enter valid date for flight departure";

        //Email
        public const string INVALID_EMAIL = "Email address is not valid";
        public const string EXISTING_EMAIL = "Email address is already is register";
        public const string MISSING_EMAIL = "User with email {0} is no register";

        //User
        public const string PASSWORD_DO_NOT_MATCH = "Incorect password";
    }
}
