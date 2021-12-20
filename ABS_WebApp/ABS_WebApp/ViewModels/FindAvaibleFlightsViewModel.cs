using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class FindAvaibleFlightsViewModel
    {
        public FindAvaibleFlightsViewModel()
        {
            Flights = new();
            Airports = new();
        }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        [Display(Name = "Origin airport:")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        [Display(Name = "Destination airport:")]
        public string Destination { get; set; }

        public string Error { get; set; }

        public List<string> Airports { get; set; }

        public List<FlightViewModel> Flights { get; set; }
    }
}
