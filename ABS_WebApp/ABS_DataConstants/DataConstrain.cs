namespace ABS_DataConstants
{
    public static class DataConstrain
    {
        //Regex expresion
        public const string EVALUATE_AIRPORT_NAME = "^[A-Z]{3}$";
        public const string EVALUATE_AIRLINE_NAME = "^[a-zA-Z]{1,5}$";
        public const string EVALUATE_FLIGHT_ID = "^[a-zA-Z0-9]+$";
        public const string EVALUATE_SEAT_COLUMN = "^[A-J]{1}$";

        //Seat
        public const int MIN_SEAT_ROWS = 1;
        public const int MIN_SEAT_COLUMNS = 1;
        public const int MAX_SEAT_ROWS = 100;
        public const int MAX_SEAT_COLUMNS = 10;
        public const string SEAT_COLUMN_TOOLTIP = "Seat column must be between 1 and 10";
        public const string SEAT_ROW_TOOlTIP = "Seat row must be between 1 and 100";

        //Api Urls
        public const string SUSTEM_API_PATH = "/api/system";
        public const string AIRPORT_API_PATH = "/api/airport";
        public const string AIRLINE_API_PATH = "/api/airline";
        public const string SEAT_API_PATH = "/api/seat";
        public const string SECTION_API_PATH = "/api/section";
        public const string FLIGHT_API_PATH = "/api/flight";
        public const string FIND_FLIGHT_API_PATH = "/api/aviableflights";

        //Cashing options
        public const int CACHE_EXPIRATION_IN_SECONDS = 10;

    }
}
