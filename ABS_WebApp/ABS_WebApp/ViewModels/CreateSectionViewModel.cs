using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class CreateSectionViewModel
    {
        public CreateSectionViewModel()
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
        [Range(minSeatRows, maxSeatRows, ErrorMessage = invalidCountRows)]
        [Display(Name = "Count or rows :")]
        public int Rows { get; set; }

        [Required]
        [Range(minSeatColms, maxSeatColms, ErrorMessage = invalidCountColumns)]
        [Display(Name = "Count of columns :")]
        public int Columns { get; set; }

        [Required]
        [Display(Name = "Type of seat class :")]
        public int SeatClass { get; set; }

        public List<string> Flights { get; set; }

        public List<string> Airlines { get; set; }
    }
}