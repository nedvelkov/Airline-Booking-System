namespace Models.Contracts
{
    public interface IFlightSection
    {

        public bool HasAvaibleSeats();

        public void BookSeat(int row, char colmn);
    }
}
