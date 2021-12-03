using System.Collections.Generic;

namespace ABS_WebApp.ViewModels
{
    public class FlightSectionViewModel
    {
        public FlightSectionViewModel() => Seats = new();
        public string Title { get; set; }
        public List<SeatViewModel> Seats { get; set; }
    }
}
