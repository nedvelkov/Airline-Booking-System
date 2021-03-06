using System.Collections.Generic;

namespace ABS_WebApp.ViewModels.DisplayObjectModels
{
    public class FlightViewModel
    {
        public FlightViewModel() => FlightSections = new();
        public string Title { get; set; }
        public List<FlightSectionViewModel> FlightSections { get; set; }
    }
}
