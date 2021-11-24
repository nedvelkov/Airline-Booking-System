namespace Facade.Models
{
    using System.Linq;
    using System.Collections.Generic;
    using Facade.Interfaces;
    using System.Text;

    class Airline:IAirline
    {
        private Dictionary<string,IFlight> _flights;

        public Airline() => this._flights = new();
        public string Name { get; init; }

        public IReadOnlyDictionary<string,IFlight> Flights => this._flights;

        public void AddFlight(IFlight flight) => _flights.Add(flight.Id, flight);

        public override string ToString()
        {
            var sb = new StringBuilder();
            var hasFlights = this.Flights.Count > 0 ? $"offers flights to over {this.Flights.Count} destinations" : "is growing and gets new offers to all destinations";
            sb.AppendLine($"Airlne {this.Name} {hasFlights}");
            if (this._flights.Count > 0)
            {
            this._flights.ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
