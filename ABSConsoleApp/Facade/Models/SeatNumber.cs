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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                SeatNumber seat = (SeatNumber)obj;
                return Row == seat.Row && Colmn==seat.Colmn;
            }
        }

        public override int GetHashCode()
        {
            return (int)Colmn * 8 / 5 + Row * 35;
        }
    }
}
