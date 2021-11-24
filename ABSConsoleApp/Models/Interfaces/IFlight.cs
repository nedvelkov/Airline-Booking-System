namespace Models.Interfaces
{

    using System;
    using System.Collections.Generic;
    using Models.Enums;

    public interface IFlight
    {
        public string Id { get; }
        public DateTime Date { get; }
        public IAirport Origin { get; }
        public IAirport Destination { get; }
        public IReadOnlyCollection<IFlightSection> FlightSections { get; }
        public void AddFlightSection(SeatClass seatClass, int rows, int colmns);

    }
}
