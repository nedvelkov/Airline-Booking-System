using System;
using System.ComponentModel.DataAnnotations;

using ABS_WebAPI.Common;
using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class FlightRequestModel
    {
        [Required]
        [RegularExpression(EVALUATE_FLIGHT_ID, ErrorMessage = FLIGHT_TOOLTIP)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        public string AirlineName { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CompareDate(ErrorMessage = INVALID_DATE)]
        public DateTime DateOfFlight { get; set; }
    }
}
