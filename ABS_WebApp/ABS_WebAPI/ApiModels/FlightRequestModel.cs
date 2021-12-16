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
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        public string Destination { get; set; }

        [Required]
        [CompareDate(ErrorMessage = notValidDate)]
        public DateTime Date { get; set; }
    }
}
