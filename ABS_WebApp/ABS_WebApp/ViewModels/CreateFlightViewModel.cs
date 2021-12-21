using System.Collections.Generic;

using ABS_Models;

namespace ABS_WebApp.ViewModels
{
    public class CreateFlightViewModel
    {
        public CreateFlightViewModel()
        {
            Airports = new();
            Airlines = new();
            Flight = new FlightModel();
        }

        public FlightModel Flight { get; set; }

        public List<string> Airlines { get; set; }

        public List<string> Airports { get; set; }
    }
}