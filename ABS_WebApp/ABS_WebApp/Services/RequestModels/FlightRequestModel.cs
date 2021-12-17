using System;

namespace ABS_WebApp.Services.RequestModels
{
    public class FlightRequestModel
    {
        public string Id { get; set; }

        public string AirlineName { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime Date { get; set; }
    }
}
