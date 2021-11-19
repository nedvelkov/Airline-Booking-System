namespace Models.Contracts
{
    using System.Collections.Generic;
    using Models.Enums;

    public interface IFlightSection
    {
        public SeatClass SeatClass { get; }
        public IReadOnlyCollection<ISeat> Seats { get; }
        public bool HasAvaibleSeats();
        public void BookSeat(int row, char colmn);
    }
}
