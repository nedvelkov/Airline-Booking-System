using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_Models
{
    public class AviableFlightsModel
    {
        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        [Display(Name = "Origin airport:")]
        public string Origin { get; set; }

        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        [Display(Name = "Destination airport:")]
        public string Destination { get; set; }
    }
}
