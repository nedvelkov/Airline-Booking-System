﻿using System.ComponentModel.DataAnnotations;

using static ABS_SystemManager.DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Error;


namespace ABS_WebApp.ViewModels
{
    public class CreateAirlineViewModel
    {
        [Required]
        [RegularExpression(evaluateAirlineName, ErrorMessage = airlineName)]
        public string Name { get; set; }
    }
}