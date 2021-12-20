﻿using System;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_Models
{
    public class BookSeatModel
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
        [Range(MIN_SEAT_ROWS, MAX_SEAT_ROWS, ErrorMessage = INVALID_SEAT_ROW)]
        [Display(Name = "Row:")]
        public int Row { get; set; }

        [Required]
        [RegularExpression(EVALUATE_SEAT_COLUMN, ErrorMessage = INVALID_SEAT_COLUMN)]
        [Display(Name = "Column:")]

        public char Column { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = INVALID_SEAT_CLASS)]
        [Display(Name = "Type of seat class:")]
        public int SeatClass { get; set; }
    }
}
