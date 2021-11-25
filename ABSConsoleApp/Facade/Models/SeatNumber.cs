namespace Facade.Models
{
    using Facade.Interfaces;

    class SeatNumber : ISeatNumber
    {
        public int Row { get; init; }

        public char Colmn { get; init; }

        public override string ToString()
        {
            return $"{Row.ToString("D3")}{Colmn}";
        }
    }
}
