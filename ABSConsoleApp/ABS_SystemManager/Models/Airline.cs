using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using ABS_SystemManager.Interfaces;
using ABS_DataConstants;

namespace ABS_SystemManager.Models
{

    internal class Airline : IAirline
    {
        private Dictionary<string, IFlight> _flights;

        public Airline() => _flights = new Dictionary<string, IFlight>();
        public string Name { get; set; }

        public IReadOnlyDictionary<string, IFlight> Flights => _flights;

        public void AddFlight(IFlight flight) => _flights.Add(flight.Id, flight);

        public override string ToString()
        {
            var sb = new StringBuilder();
            var airlineData = Flights.Count > 0 ? String.Format(DataConstrain.airlineWithFlights, Flights.Count) : DataConstrain.airlineWithNoFlights;

            sb.AppendLine(String.Format(DataConstrain.airlineToStringTitle, Name, airlineData));

            if (_flights.Count > 0)
            {
                _flights.ToList().ForEach(x => sb.AppendLine(x.Value.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
