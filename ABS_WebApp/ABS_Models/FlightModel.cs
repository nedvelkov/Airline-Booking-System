using System;
using System.ComponentModel.DataAnnotations;

using ABS_Models.Common;
using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_Models
{
    public class FlightModel
    {
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
        [DataType(DataType.Date)]
        [CompareDate(ErrorMessage = INVALID_DATE)]
        [Display(Name = "Date of flight:")]
        public DateTime DateOfFlight { get; set; }
    }
}
