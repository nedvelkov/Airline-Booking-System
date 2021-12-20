namespace ABS_DataConstants
{
    public static class Error
    {
        //Common
        public const string DUBLICATE_ITEM = "{0} with this {1} already exist";
        public const string MISSING_ITEM = "{0} with {1} {2} don't exist";

        //Airport
        public const string AIRPORT_TOOLTIP = "Airport name must be 3 upper letters";

        //Airlane
        public const string AIRLINE_TOOLTIP = "Name of airline must have only letters and length between 1 and 5 characters";

        //Flight
        public const string FLIGHT_TOOLTIP = "Flight id must be with numbers and letters only";

        //SeatClass
        public const string INVALID_SEAT_CLASS = "Seat class is not valid";
        public const string INVALID_COUNT_OF_SEATS = "{0} of seat must be between {1} and {2}";

        //Seat
        public const string INVALID_SEAT_ROW = "Invalid seat row";
        public const string INVALID_SEAT_COLUMN = "Invalid seat column";

        //Date
        public const string INVALID_DATE = "Date is not valid";
    }
}
