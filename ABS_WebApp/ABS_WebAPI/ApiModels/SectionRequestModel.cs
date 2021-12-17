﻿using System;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class SectionRequestModel
    {
        [Required]
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        public string AirlineName { get; set; }

        [Required]
        [Range(minSeatRows, maxSeatRows, ErrorMessage = invalidCountRows)]
        public int Rows { get; set; }

        [Required]
        [Range(minSeatColms, maxSeatColms, ErrorMessage = invalidCountColumns)]
        public int Columns { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = invalidSeatClass)]
        public int SeatClass { get; set; }
    }
}