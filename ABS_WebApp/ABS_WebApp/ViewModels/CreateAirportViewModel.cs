using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;


namespace ABS_WebApp.ViewModels
{
    public class CreateAirportViewModel
    {
        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Airport name:")]
        public string Name { get; set; }
    }
}
