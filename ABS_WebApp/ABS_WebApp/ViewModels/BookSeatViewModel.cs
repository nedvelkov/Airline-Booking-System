using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class BookSeatViewModel
    {
        [Required]
        [RegularExpression(evaluateFlightId, ErrorMessage = flightId)]
        [Display(Name = "Identification number of flight")]
        public string Id { get; set; }

        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        [Display(Name = "Airline name")]
        public string AirlineName { get; set; }

        [Required]
        [Range(minSeatRows, maxSeatRows, ErrorMessage = invalidSeatRow)]
        public int Row { get; set; }

        [Required]
        [RegularExpression(evaluateSeatColumn, ErrorMessage = invalidSeatColumn)]
        public string Column { get; set; }

        [Required]
        [Display(Name = "Type of seat class")]
        public int SeatClass { get; set; }
    }
}
//string BookSeat(string airlineName, string flightId, int seatClass, int row, char column)