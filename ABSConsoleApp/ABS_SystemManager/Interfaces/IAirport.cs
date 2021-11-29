namespace ABS_SystemManager.Interfaces
{

    using System.Collections.Generic;

    interface IAirport
    {
        public string Name { get; }

        public IReadOnlyList<string> DeparturesFlights();

        public void AddDeparureFlight(string flightId);

        public IReadOnlyList<string> ArrivalFlights();

        public void AddArrivalFlight(string flightId);
    }
}
