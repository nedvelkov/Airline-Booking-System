using System;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class SectionRequestModel
    {
        [Required]
        [RegularExpression(EVALUATE_FLIGHT_ID, ErrorMessage = FLIGHT_TOOLTIP)]
        public string Id { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        public string AirlineName { get; set; }

        [Required]
        [Range(MIN_SEAT_ROWS, MAX_SEAT_ROWS, ErrorMessage = SEAT_ROW_TOOlTIP)]
        public int Rows { get; set; }

        [Required]
        [Range(MIN_SEAT_COLUMNS, MAX_SEAT_COLUMNS, ErrorMessage = SEAT_COLUMN_TOOLTIP)]
        public int Columns { get; set; }

        [Required]
        [Range(1, 3, ErrorMessage = INVALID_SEAT_CLASS)]
        public int SeatClass { get; set; }
    }
}
