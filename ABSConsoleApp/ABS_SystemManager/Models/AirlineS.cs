using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ABS_SystemManager.Interfaces;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.Models
{

    internal class AirlineS : IAirline
    {
        private Dictionary<string, IFlight> _flights;

        public AirlineS() => _flights = new Dictionary<string, IFlight>();
        public string Name { get; set; }

        public IReadOnlyDictionary<string, IFlight> Flights => _flights;

        public void AddFlight(IFlight flight) => _flights.Add(flight.Id, flight);

        public override string ToString()
        {
            var sb = new StringBuilder();
            var airlineData = Flights.Count > 0 ? String.Format(AIRLINE_WITH_FLIGHTS_TO_STRING, Flights.Count) : AIRLINE_WITH_NO_FLIGHT_TO_STRING;

            sb.AppendLine(String.Format(AIRLINE_TO_STRING_TITILE, Name, airlineData));

            if (_flights.Count > 0)
            {
                _flights.ToList().ForEach(x => sb.AppendLine(x.Value.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
