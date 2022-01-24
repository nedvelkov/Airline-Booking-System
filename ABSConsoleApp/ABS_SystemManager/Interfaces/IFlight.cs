using System;
using System.Collections.Generic;

namespace ABS_SystemManager.Interfaces
{
    internal interface IFlight
    {
        public string Id { get; }

        public DateTime Date { get; }

        public IAirline Airline { get; }

        public IReadOnlyDictionary<SeatClass, IFlightSection> FlightSections { get; }

        public void AddFlightSection(IFlightSection flightSection);
    }
}
