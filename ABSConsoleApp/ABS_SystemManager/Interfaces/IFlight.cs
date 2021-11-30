namespace ABS_SystemManager.Interfaces
{
    using System;
    using System.Collections.Generic;

    internal interface IFlight
    {
        public string Id { get; }

        public DateTime Date { get; }

        public IAirport Origin { get; }

        public IAirport Destination { get; }

        public IAirline Airline { get; }

        public IReadOnlyDictionary<SeatClass, IFlightSection> FlightSections { get; }

        public void AddFlightSection(IFlightSection flightSection);
    }
}
