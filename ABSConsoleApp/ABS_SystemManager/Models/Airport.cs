using System;
using System.Collections.Generic;

using ABS_SystemManager.Interfaces;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.Models
{

    internal class Airport : IAirport
    {
        private List<string> _departuresFlights;
        private List<string> _arriavalFlights;
        public Airport()
        {
            _departuresFlights = new List<string>();
            _arriavalFlights = new List<string>();
        }

        public string Name { get; set; }

        public IReadOnlyList<string> DeparturesFlights() => _departuresFlights;

        public void AddDeparureFlight(string flightId) => _departuresFlights.Add(flightId);

        public IReadOnlyList<string> ArrivalFlights() => _arriavalFlights;

        public void AddArrivalFlight(string flightId) => _arriavalFlights.Add(flightId);

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
            return hash / 2;
        }

        public override string ToString()
        {
            return String.Format(AIRPORT_TO_STRING_TITLE, Name);
        }

    }
}
