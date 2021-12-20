using System;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class BookSeatRequestModel
    {
        [Required]
        [RegularExpression(EVALUATE_FLIGHT_ID, ErrorMessage = FLIGHT_TOOLTIP)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        public string AirlineName { get; set; }

        [Required]
        [Range(MIN_SEAT_ROWS, MAX_SEAT_ROWS, ErrorMessage = INVALID_SEAT_ROW)]
        public int Row { get; set; }

        [Required]
        [RegularExpression(EVALUATE_SEAT_COLUMN, ErrorMessage = INVALID_SEAT_COLUMN)]
        public char Column { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = INVALID_SEAT_CLASS)]
        public int SeatClass { get; set; }
    }
}
