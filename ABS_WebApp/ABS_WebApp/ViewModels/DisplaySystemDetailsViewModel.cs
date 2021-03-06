using System.Collections.Generic;
using ABS_WebApp.ViewModels.DisplayObjectModels;

namespace ABS_WebApp.ViewModels
{
    public class DisplaySystemDetailsViewModel
    {
        public DisplaySystemDetailsViewModel()
        {
            AirporstList = new();
            AirlinesList = new();
        }
        public string AirportsTitle { get; set; }

        public List<string> AirporstList { get; set; }

        public string AirlinesTitle { get; set; }

        public List<AirlineViewModel> AirlinesList { get; set; }

    }
}
