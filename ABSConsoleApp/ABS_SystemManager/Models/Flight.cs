using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using ABS_SystemManager.Interfaces;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.Models
{

    internal class Flight : IFlight
    {
        private Dictionary<SeatClass, IFlightSection> _flightSections;

        public Flight() => _flightSections = new Dictionary<SeatClass, IFlightSection>();

        public string Id { get; set; }

        public DateTime Date { get; set; }

        public IAirport Origin { get; set; }

        public IAirport Destination { get; set; }

        public IAirline Airline { get; set; }

        public IReadOnlyDictionary<SeatClass, IFlightSection> FlightSections => _flightSections;

        public void AddFlightSection(IFlightSection flightSection) => _flightSections.Add(flightSection.SeatClass, flightSection);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(FLIGHT_TO_STRING_TITLE, Id, Origin.Name, Destination.Name, Date.ToString(FORMAT_FOR_DATE_TIME)));
            _flightSections.ToList().ForEach(x => sb.AppendLine(x.Value.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
