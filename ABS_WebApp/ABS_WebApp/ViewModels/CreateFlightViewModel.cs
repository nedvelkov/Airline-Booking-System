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
        [RegularExpression(EVALUATE_FLIGHT_ID, ErrorMessage = FLIGHT_TOOLTIP)]
        [Display(Name = "Identification number of flight:")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        [Display(Name = "Airline name:")]
        public string AirlineName { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        [Display(Name = "Origin airport:")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
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