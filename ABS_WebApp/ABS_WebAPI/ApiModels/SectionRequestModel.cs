using System;
using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebAPI.ApiModels
{
    public class SectionRequestModel
    {
        [Required]
        [Range(minSeatRows, maxSeatRows, ErrorMessage = invalidCountRows)]
        public int Rows { get; set; }

        [Required]
        [Range(minSeatColms, maxSeatColms, ErrorMessage = invalidCountColumns)]
        public int Columns { get; set; }
    }
}
