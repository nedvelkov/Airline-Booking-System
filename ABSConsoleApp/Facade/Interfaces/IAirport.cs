namespace Facade.Interfaces
{

    using System.Collections.Generic;

    interface IAirport
    {
        public string Name { get; }

        public IReadOnlyDictionary<string,IFlight> DeparturesFlights();

        public void AddDeparureFlight(IFlight flight);
    }
}
