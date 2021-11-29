namespace ABS_SystemManager.Models
{
    using ABS_SystemManager.Interfaces;

    class SeatNumber : ISeatNumber
    {
        public int Row { get; init; }

        public char Column { get; init; }

        public override string ToString()
        {
            return $"{Row.ToString("D3")}{Column}";
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
                return Row == seat.Row && Column==seat.Column;
            }
        }

        public override int GetHashCode()
        {
            return (int)Column * 8 / 5 + Row * 35;
        }
    }
}
