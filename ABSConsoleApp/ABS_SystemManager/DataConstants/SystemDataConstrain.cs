namespace ABS_SystemManager.DataConstants
{
    public static class SystemDataConstrain
    {
        //Airport
        public const string AIRPORT_TO_STRING_TITLE = "-AP Airport {0} is available for all airlines and flights";

        //Airline
        public const string AIRLINE_WITH_NO_FLIGHT_TO_STRING = "is awaits offers for new destinations";
        public const string AIRLINE_WITH_FLIGHTS_TO_STRING = "offers flights to over {0} destinations";

        /// <summary>
        /// String for first line of toString method.
        /// 0 - airline name, 1 - airline data.
        /// </summary>
        public const string AIRLINE_TO_STRING_TITILE = "-AL Airline {0} {1}";

        //Flight
        /// <summary>
        /// String for first line of toSting method.
        /// 0 - flight id, 1 - origin, 2 - destination, 3 - date.
        /// </summary>
        public const string FLIGHT_TO_STRING_TITLE = "-FL Flight #{0} from {1} to {2}.Departure at {3}";
        public const string FLIGHT_SECTIONS_TITLE = "=FL The flight has {0} section.";

        //Flight section
        /// <summary>
        /// String for first line of toSting method.
        /// 0 - seat class, 1 - seats count.
        /// </summary>
        public const string FLIGHT_SECTION_TO_STRING_TITLE = "-FS Flight section {0} class with {1} seats";

        //Seat

        public const char FIRST_SEAT_COLUMN_AS_CHAR = 'A';
        public const char LAST_SEAT_COLUMN_AS_CHAR = 'J';
        public const string SEAT_NUMBER_TO_STRING = "{0:D3}{1}";

        /// <summary>
        /// This value plus count of column gives right column letter
        /// </summary>
        public const int INITIAL_VALUE_FOR_SEAT_COLUMN_CHAR = 65;


        //System manager
        public const string DISPLAY_AIRPORTS_TITLE = "=AP Airports aviable {0}";
        public const string DISPLAY_AIRLINES_TITLE = "=AL Airlines aviable {0}";

        //DateTime formating
        public const string FORMAT_FOR_DATE_TIME = "MM/dd/yyyy";
    }
}
