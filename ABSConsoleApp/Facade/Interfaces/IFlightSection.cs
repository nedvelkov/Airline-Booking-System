namespace Facade.Interfaces
{
    using System.Collections.Generic;

    interface IFlightSection
    {
        public SeatClass SeatClass { get; }

        public IReadOnlyDictionary<ISeatNumber,ISeat> Seats { get; }

        public bool HasAvaibleSeats();

        public void BookSeat(ISeatNumber number);

        public void AddSeat(IEnumerable<ISeat> seats);
    }
}
