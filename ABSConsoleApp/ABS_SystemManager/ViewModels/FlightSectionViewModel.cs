using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.ViewModels
{

    public class FlightSectionViewModel
    {
        public FlightSectionViewModel() => Seats = new HashSet<SeatViewModel>();
        public SeatClass SeatClass { get; set; }

        public ICollection<SeatViewModel> Seats { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(FLIGHT_SECTION_TO_STRING_TITLE, SeatClass, Seats.Count));
            Seats.ToList().ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }

    }
}
