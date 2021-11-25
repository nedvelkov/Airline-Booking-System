namespace Facade.DataConstants
{
    public static class DataConstrain
    {
        //Regex expresion

        public const string evaluateAirportName = "^[A-Z]{3}$";
        public const string evaluateAirlineName = "^[a-zA-Z]{1,5}$";
        public const string evaluateFlightId = "^[a-zA-Z0-9]+$";

        //Seat
        public const int minSeatRows = 1;
        public const int minSeatColms = 1;
        public const int maxSeatRows = 100;
        public const int maxSeatColms = 10;
        public const char firstSeatChar = 'A';
        public const char lastSeatChar = 'J';

        /// <summary>
        /// This value plus count of column gives right column letter
        /// </summary>
        public const int initialValueForSeatColm = 64;

        //DateTime formating
        public const string formatDateTime = "MM/dd/yyyy";

    }
}
