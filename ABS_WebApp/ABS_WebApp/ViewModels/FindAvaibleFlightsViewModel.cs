using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class FindAvaibleFlightsViewModel
    {
        public FindAvaibleFlightsViewModel() => Flights = new List<string>();

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Origin airport")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Destination airport")]
        public string Destination { get; set; }

        public List<string> Flights { get; set; }
    }
}
