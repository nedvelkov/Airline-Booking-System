using System;
using System.ComponentModel.DataAnnotations;

using ABS_WebApp.Common;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class CreateFlightViewModel
    {
        [Required]
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        [Display(Name = "Identification number of flight")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        [Display(Name = "Airline name")]
        public string AirlineName { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Origin airport")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Destination airport")]
        public string Destination { get; set; }

        [Required]
        [CompareDate(ErrorMessage = notValidDate)]
        [Display(Name = "Date of flight")]
        public DateTime Date { get; set; }
    }
}