using System.Collections.Generic;

namespace ABS_WebApp.ViewModels
{
    public class FlightSectionViewModel
    {
        public string Title { get; set; }
        public SeatViewModel[,] Seats { get; set; }
    }
}
