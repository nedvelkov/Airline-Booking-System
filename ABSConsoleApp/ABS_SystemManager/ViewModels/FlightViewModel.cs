using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.ViewModels
{

    public class FlightViewModel
    {

        public FlightViewModel() => FlightSections = new HashSet<FlightSectionViewModel>();

        public string FlightId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }

        public ICollection<FlightSectionViewModel> FlightSections { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(FLIGHT_TO_STRING_TITLE, FlightId, Origin, Destination));
            FlightSections.ToList().ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
