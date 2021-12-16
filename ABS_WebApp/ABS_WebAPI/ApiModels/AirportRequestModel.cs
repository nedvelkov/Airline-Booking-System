using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class AirportRequestModel
    {
        [Required]
        [RegularExpression(evaluateAirportName, ErrorMessage = airportName)]
        public string Name { get; set; }
    }
}
