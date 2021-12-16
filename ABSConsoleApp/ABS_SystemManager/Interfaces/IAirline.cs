using System.Collections.Generic;

namespace ABS_SystemManager.Interfaces
{
    internal interface IAirline
    {
        public string Name { get; }

        public IReadOnlyDictionary<string, IFlight> Flights { get; }

        public void AddFlight(IFlight flight);
    }
}
