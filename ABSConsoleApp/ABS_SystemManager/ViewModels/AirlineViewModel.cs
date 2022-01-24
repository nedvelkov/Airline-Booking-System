using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.ViewModels
{

    public class AirlineViewModel
    {
        public AirlineViewModel() => Flights = new HashSet<FlightViewModel>();
        public string AirlineName { get; set; }
        public ICollection<FlightViewModel> Flights { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            var airlineData = Flights.Count > 0 ? String.Format(AIRLINE_WITH_FLIGHTS_TO_STRING, Flights.Count) : AIRLINE_WITH_NO_FLIGHT_TO_STRING;

            sb.AppendLine(String.Format(AIRLINE_TO_STRING_TITILE, AirlineName, airlineData));

            if (Flights.Count > 0)
            {
                Flights.ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
