using System.Collections.Generic;

using ABS_Models;
using ABS_WebApp.ViewModels.DisplayObjectModels;


namespace ABS_WebApp.ViewModels
{
    public class FindAvaibleFlightsViewModel
    {
        public FindAvaibleFlightsViewModel()
        {
            Flights = new();
            Airports = new();
            Flight = new AviableFlightsModel();
        }

        public AviableFlightsModel Flight { get; set; }

        public string Error { get; set; }

        public List<string> Airports { get; set; }

        public List<FlightViewModel> Flights { get; set; }
    }
}
