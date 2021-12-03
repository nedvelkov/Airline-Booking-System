using System.Collections.Generic;

namespace ABS_WebApp.ViewModels
{
    public class DisplaySystemDetailsViewModel
    {
        public DisplaySystemDetailsViewModel()
        {
            AirporstList = new();
            AirlinesList = new();
            Details = new();
        }

        public List<string>Details { get; set; }
        public string AirportsTitle { get; set; }

        public List<string> AirporstList { get; set; }

        public string AirlinesTitle { get; set; }

        public List<AirlineViewModel> AirlinesList { get; set; }

    }
}
