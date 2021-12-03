using System.Collections.Generic;

namespace ABS_WebApp.ViewModels
{
    public class AirlineViewModel
    {
        public AirlineViewModel() => Flights = new();
        public string Title { get; set; }
        public List<FlightViewModel> Flights { get; set; }
    }
}
