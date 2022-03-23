using System.Collections.Generic;

namespace ABS_Models
{
    public class SystemDetailsModel
    {
        public SystemDetailsModel()
        {
            AirportList = new List<string>();
            AirlineList = new List<AirlineSystemDisplay>();
        }
        public List<string> AirportList { get; set; }
        public List<AirlineSystemDisplay> AirlineList { get; set; }
    }
}
