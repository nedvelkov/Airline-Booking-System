namespace ABS_SystemManager.Interfaces
{
    internal interface ISeat
    {
        public int Row { get; }
        public char Column { get; }
        public bool Booked { get; }
        public void BookSeat();
    }
}
