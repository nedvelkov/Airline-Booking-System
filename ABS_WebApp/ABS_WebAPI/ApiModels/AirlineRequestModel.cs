using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class AirlineRequestModel
    {
        [Required]
        [RegularExpression(EVALUATE_AIRLINE_NAME, ErrorMessage = AIRLINE_TOOLTIP)]
        public string Name { get; set; }
    }
}
