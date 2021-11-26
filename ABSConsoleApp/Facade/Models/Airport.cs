namespace Facade.Models
{
    using System;
    using System.Collections.Generic;

    using Facade.Interfaces;
    using static Facade.DataConstants.DataConstrain;

    class Airport:IAirport
    {
        private Dictionary<string, IFlight> _departuresFlights;
        public Airport() => _departuresFlights = new();
        public string Name { get; init; }

        public IReadOnlyDictionary<string, IFlight> DeparturesFlights() => _departuresFlights;

        public void AddDeparureFlight(IFlight flight) => _departuresFlights.Add(flight.Id, flight);


        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Airport airport = (Airport)obj;
                return Name == airport.Name;
            }
        }

        public override int GetHashCode()
        {
            var hash = 0;
            foreach (var letter in Name)
            {
                hash += (int)letter * 4;
            }
            return hash/2;
        }

        public override string ToString()
        {
            return String.Format(airportToStringTitle,Name);
        }

    }
}
