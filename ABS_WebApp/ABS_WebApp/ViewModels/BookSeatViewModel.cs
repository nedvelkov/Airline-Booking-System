using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class BookSeatViewModel
    {
        public BookSeatViewModel()
        {
            Flights = new();
            Airlines = new();
        }

        [Required]
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        [Display(Name = "Identification number of flight :")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        [Display(Name = "Airline name :")]
        public string AirlineName { get; set; }

        [Required]
        [Range(minSeatRows, maxSeatRows, ErrorMessage = invalidSeatRow)]
        [Display(Name = "Row :")]
        public int Row { get; set; }

        public string RowHelp => seatRowHelp;

        [Required]
        [RegularExpression(evaluateSeatColumn, ErrorMessage = invalidSeatColumn)]
        [Display(Name = "Column :")]
        public string Column { get; set; }

        [Required]
        [Display(Name = "Type of seat class :")]
        public int SeatClass { get; set; }

        public List<string> Flights { get; set; }

        public List<string> Airlines { get; set; }
    }
}