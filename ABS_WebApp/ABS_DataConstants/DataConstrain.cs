namespace ABS_DataConstants
{
    public static class DataConstrain
    {
        //Regex expresion
        public const string EVALUATE_AIRPORT_NAME = "^[A-Z]{3}$";
        public const string EVALUATE_AIRLINE_NAME = "^[a-zA-Z]{1,5}$";
        public const string EVALUATE_FLIGHT_ID = "^[a-zA-Z0-9]+$";
        public const string EVALUATE_SEAT_COLUMN = "^[A-J]{1}$";
        public const string EVALUATE_PASSWORD = "^[a-zA-Z0-9]{8,}$";
        public const string EVALUATE_USERNAME = "^[a-zA-Z]{1,}$";

        //Seat
        public const int MIN_SEAT_ROWS = 1;
        public const int MIN_SEAT_COLUMNS = 1;
        public const int MAX_SEAT_ROWS = 100;
        public const int MAX_SEAT_COLUMNS = 10;
        public const string SEAT_COLUMN_TOOLTIP = "Seat column must be between 1 and 10";
        public const string SEAT_ROW_TOOlTIP = "Seat row must be between 1 and 100";

        //Api Urls
        public const string SUSTEM_API_PATH = "/api/system";
        public const string AIRPORT_API_PATH = "/api/airports";
        public const string AIRLINE_API_PATH = "/api/airlines";
        public const string SEAT_API_PATH = "/api/seats";
        public const string SECTION_API_PATH = "/api/sections";
        public const string FLIGHT_API_PATH = "/api/flights";
        public const string FIND_FLIGHT_API_PATH = "/api/aviableflights";
        public const string ACCOUNT_API_PATH = "/api/accounts";
        public const string USER_REGISTER = "register";
        public const string USER_LOGIN = "login";

        //Cashing options
        public const int SHARED_CACHE_EXPIRATION_IN_SECONDS = 10;

        //Successfull operation
        public const string SUCCESSFULL_OPERATION = "successfully";

        //Cookies
        public const string COOKIE_SHEME_NAME = "Cookie";
        public const string COOKIE_TOKEN_NAME = "auth_cookie";

        //Roles
        public const string ADMIN_ROLE = "Admin";
        public const string USER_ROLE = "User";
    }
}
