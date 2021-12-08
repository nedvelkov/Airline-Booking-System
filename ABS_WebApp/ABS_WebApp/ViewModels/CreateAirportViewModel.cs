using System.ComponentModel.DataAnnotations;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;


namespace ABS_WebApp.ViewModels
{
    public class CreateAirportViewModel
    {
        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        [Display(Name = "Airport name :")]
        public string Name { get; set; }

        public string NameHelp => airportName;
    }
}
