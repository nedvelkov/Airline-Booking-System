namespace Facade.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Facade.Interfaces;

    class Flight:IFlight
    {
        private Dictionary<SeatClass,IFlightSection> _flightSections;

        public Flight() => this._flightSections = new();

        public string Id { get; init; }

        public DateTime Date { get; set; }

        public IAirport Origin { get; set; }

        public IAirport Destination { get; set; }

        public IReadOnlyDictionary<SeatClass,IFlightSection> FlightSections => _flightSections;

        public void AddFlightSection(IFlightSection flightSection) => _flightSections.Add(flightSection.SeatClass, flightSection);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight #{this.Id} from {this.Origin.Name} to {this.Destination.Name}.Departure at {this.Date.ToString("MM/dd/yyyy")}");
            sb.AppendLine($"The flight has {this._flightSections.Count} section.");
            this._flightSections.ToList().ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
