namespace Models.Contracts
{

    using Models.Enums;

    public interface IFlight
    {
        public void AddFlightSection(SeatClass seatClass, int rows, int colmns);
        
    }
}
