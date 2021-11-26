namespace Facade.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Facade.Interfaces;
    using static DataConstants.DataConstrain;

    class Flight:IFlight
    {
        private Dictionary<SeatClass,IFlightSection> _flightSections;

        public Flight() => _flightSections = new();

        public string Id { get; init; }

        public DateTime Date { get; set; }

        public IAirport Origin { get; set; }

        public IAirport Destination { get; set; }

        public IAirline Airline { get; init; }

        public IReadOnlyDictionary<SeatClass,IFlightSection> FlightSections => _flightSections;

        public void AddFlightSection(IFlightSection flightSection) => _flightSections.Add(flightSection.SeatClass, flightSection);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(flightToStringTitle,Id,Origin.Name,Destination.Name,Date.ToString(formatDateTime)));
            sb.AppendLine(String.Format(flightSectionCount,_flightSections.Count));
            _flightSections.ToList().ForEach(x => sb.AppendLine(x.Value.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
