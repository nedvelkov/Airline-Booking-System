using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;


namespace ABS_WebApp.ViewModels
{
    public class CreateAirlineViewModel
    {
        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        [Display(Name = "Airline name:")]
        public string Name { get; set; }

    }
}
