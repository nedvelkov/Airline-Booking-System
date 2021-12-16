using System.Collections.Generic;

namespace ABS_SystemManager.Interfaces
{

    internal interface IAirport
    {
        public string Name { get; }

        public IReadOnlyList<string> DeparturesFlights();

        public void AddDeparureFlight(string flightId);

        public IReadOnlyList<string> ArrivalFlights();

        public void AddArrivalFlight(string flightId);
    }
}
