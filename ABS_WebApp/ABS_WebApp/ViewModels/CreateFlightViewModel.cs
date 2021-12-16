using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class CreateFlightViewModel
    {
        public CreateFlightViewModel()
        {
            Airports = new();
            Airlines = new();
        }

        [Required]
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        [Display(Name = "Identification number of flight:")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        [Display(Name = "Airline name:")]
        public string AirlineName { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Origin airport:")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Destination airport:")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Date of flight:")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public List<string> Airlines { get; set; }

        public List<string> Airports { get; set; }
    }
}