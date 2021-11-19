namespace Models.Contracts
{
    public interface ISeat
    {
        public int Row { get; }
        public char Colmn { get; }
        public bool Booked { get; }
        public void BookSeat();
    }
}
