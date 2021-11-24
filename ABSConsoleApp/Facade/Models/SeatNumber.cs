namespace Facade.Models
{
    using Facade.Interfaces;

    class SeatNumber : ISeatNumber
    {
        public int Row { get; init; }

        public char Colmn { get; init; }
    }
}
