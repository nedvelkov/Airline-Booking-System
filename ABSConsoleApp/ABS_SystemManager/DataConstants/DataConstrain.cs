namespace ABS_SystemManager.DataConstants
{
    public static class DataConstrain
    {
        //Regex expresion

        public const string evaluateAirportName = "^[A-Z]{3}$";
        public const string evaluateAirlineName = "^[a-zA-Z]{1,5}$";
        public const string evaluateFlightId = "^[a-zA-Z0-9]+$";
        public const string evaluateSeatColumn = "^[A-J]{1}$";

        //Airport
        public const string airportToStringTitle = "Airport {0} is available for all airlines and flights";

        //Airline
        public const string airlineWithNoFlights = "is awaits offers for new destinations";
        public const string airlineWithFlights = "offers flights to over {0} destinations";

        /// <summary>
        /// String for first line of toString method.
        /// 0 - airline name, 1 - airline data.
        /// </summary>
        public const string airlineToStringTitle = "Airline {0} {1}";

        //Flight
        /// <summary>
        /// String for first line of toSting method.
        /// 0 - flight id, 1 - origin, 2 - destination, 3 - date.
        /// </summary>
        public const string flightToStringTitle = "Flight #{0} from {1} to {2}.Departure at {3}";
        public const string flightSectionCount = "The flight has {0} section.";

        //Flight section
        /// <summary>
        /// String for first line of toSting method.
        /// 0 - seat class, 1 - seats count.
        /// </summary>
        public const string flightSectionToStringTitle = "Flight section {0} class with {1} seats";

        //Seat
        public const int minSeatRows = 1;
        public const int minSeatColms = 1;
        public const int maxSeatRows = 100;
        public const int maxSeatColms = 10;
        public const char firstSeatChar = 'A';
        public const char lastSeatChar = 'J';
        public const string seatNumber = "{0:D3}{1}";

        /// <summary>
        /// This value plus count of column gives right column letter
        /// </summary>
        public const int initialValueForSeatColumn = 65;

        /// <summary>
        /// This value minus letter of column as integer gives right column possition in array.
        /// /// </summary>
        public const int valueForSeatColumn = 65;

        //System manager
        public const string displayAirportsTitle= "Airports aviable {0}";
        public const string displayAirlinesTitle= "Airlines aviable {0}";

        //DateTime formating
        public const string formatDateTime = "MM/dd/yyyy";

        //Engine constants
        public const string helpCommand = "Create object command: -create\r\n" +
                                            "Find flight command: -find\r\n" +
                                            "Book seat command: -book\r\n" +
                                            "Display system detail command: -details\r\n" +
                                            "Helm menu command: --help\r\n" +
                                            "Go to main menu command: --main\r\n" +
                                            "Close program command: --exit\r\n" +
                                            "Cansel current operation and return main menu press \"esc\" button";

        public const int breakVoid = -257;
        public const string titleConsole = "ABS - Airline Bookig System";
        public const string returnToMainMenu = "Do you want to return in main menu (Y/N): ";

    }
}
