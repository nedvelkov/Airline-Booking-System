namespace ABS_SystemManager.Interfaces
{
    internal interface IFlightSection
    {
        public SeatClass SeatClass { get; }

        public ISeat[,] Seats { get; }

        public bool HasAvaibleSeats();

        public void BookSeat(int row, int column);

        public void AddSeats(ISeat[,] seats);
    }
}
