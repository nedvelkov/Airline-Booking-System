namespace ABSConsoleApp
{
    using System;
    using ABS_SystemManager;

    public class Program
    {
        public static void Main()
        {
             Example();

            //var engine = new Engine();
            //engine.Run();
        }

        /// <summary>
        /// This method is based on task description.
        /// Changes:
        /// Dates on flight are change to be valid.
        /// Seat section are created based on int values, not on SeatClass enum.
        /// Flight id is accpted to be unique,and some data is invalid in example.
        /// </summary>
        public static void Example()
        {
            var res = new SystemManager();

            //Create airports
            res.CreateAirport("DEN");
            res.CreateAirport("DFW");
            res.CreateAirport("LON");
            res.CreateAirport("JPN");
            res.CreateAirport("DE"); //invalid
            res.CreateAirport("DEH");
            res.CreateAirport("DEN");
            res.CreateAirport("NCE");
            res.CreateAirport("TRIord9"); //invalid
            res.CreateAirport("DEN");

            //Create airlines
            res.CreateAirline("DELTA");
            res.CreateAirline("AMER");
            res.CreateAirline("JET");
            res.CreateAirline("DELTA");
            res.CreateAirline("SWEST");
            res.CreateAirline("AMER");
            res.CreateAirline("FRONT");
            res.CreateAirline("FRONTIER"); //invalid

            //Create flights
            var date = DateTime.UtcNow.AddDays(10);
            res.CreateFlight("DELTA", "DEN", "LON", date.Year, date.Month, date.Day, "123");
            res.CreateFlight("DELTA", "DEN", "DEH", date.Year, date.Month, date.Day, "567");
            res.CreateFlight("DELTA", "DEN", "NCE", date.Year, date.Month, date.Day, "567");    //invalid
            res.CreateFlight("JET", "LON", "DEN", date.Year, date.Month, date.Day, "123b");     //change id to be accpted
            res.CreateFlight("AMER", "DEN", "LON", date.Year, date.Month, date.Day, "123c");    //change id to be accpted
            res.CreateFlight("JET", "DEN", "LON", date.Year, date.Month, date.Day, "786");
            res.CreateFlight("JET", "DEN", "LON", date.Year, date.Month, date.Day, "909");

            //Create sections
            res.CreateSection("JET", "123b", 2, 2, 3);       //change id to be accpted
            res.CreateSection("JET", "123b", 1, 3, 3);       //change id to be accpted
            res.CreateSection("JET", "123b", 2, 3, 1);       //change id to be accpted
            res.CreateSection("DELTA", "123", 1, 1, 2);
            res.CreateSection("DELTA", "123", 1, 2, 3);
            res.CreateSection("SWSERTT", "123", 5, 5, 3);  //invalid

            res.DisplaySystemDetails();

            res.FindAvailableFlights("DEN", "LON");

            res.BookSeat("DELTA", "123", 2, 1, 'A');
            res.BookSeat("DELTA", "123", 3, 1, 'A');
            res.BookSeat("DELTA", "123", 3, 1, 'B');
            res.BookSeat("DELTA", "123", 2, 1, 'A');  //already booked

            res.DisplaySystemDetails();

            res.FindAvailableFlights("DEN", "LON");
        }
    }
}
