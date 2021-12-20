using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class AirportRequestModel
    {
        [Required]
        [RegularExpression(EVALUATE_AIRPORT_NAME, ErrorMessage = AIRPORT_TOOLTIP)]
        public string Name { get; set; }
    }
}
