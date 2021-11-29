namespace ABS_SystemManager.Interfaces
{
    interface ISeat
    {
        public ISeatNumber Number { get; }
        public bool Booked { get; }
        public void BookSeat();
    }
}
