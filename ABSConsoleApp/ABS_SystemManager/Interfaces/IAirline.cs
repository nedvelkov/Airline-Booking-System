﻿namespace ABS_SystemManager.Interfaces
{
    using System.Collections.Generic;
    internal interface IAirline
    {
        public string Name { get; }

        public IReadOnlyDictionary<string, IFlight> Flights { get; }

        public void AddFlight(IFlight flight);
    }
}
