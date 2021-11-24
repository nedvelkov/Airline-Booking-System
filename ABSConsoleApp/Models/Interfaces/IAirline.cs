namespace Models.Interfaces
{
    using System.Collections.Generic;
    public interface IAirline
    {
        public string Name { get; }
        public IReadOnlyCollection<IFlight> Flights { get; }
        public void AddFlight(IFlight flight);
    }
}
