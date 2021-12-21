using System.Collections.Generic;

using ABS_Models;

namespace ABS_WebApp.ViewModels
{
    public class CreateSectionViewModel
    {
        public CreateSectionViewModel()
        {
            Flights = new();
            Airlines = new();
            FlightSection = new FlightSectionModel();
        }

        public FlightSectionModel FlightSection { get; set; }

        public List<string> Flights { get; set; }

        public List<string> Airlines { get; set; }
    }
}