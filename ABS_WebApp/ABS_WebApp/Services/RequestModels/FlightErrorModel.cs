using System;

namespace ABS_WebApp.Services.RequestModels
{
    public class FlightErrorModel
    {
        public string Id { get; set; }

        public string AirlineName { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public string DateOfFlight { get; set; }
    }
}
