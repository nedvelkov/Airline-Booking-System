namespace ABS_SystemManager.Models
{
    using System;
    using ABS_SystemManager.Interfaces;

    internal class Seat : ISeat
    {
        private bool _booked;

        public int Row { get; init; }
        public char Column { get; init; }

        public bool Booked => _booked;

        public void BookSeat() => _booked = true;

        public override string ToString()
        {
            var seatIsBooked = Booked ? "booked" : "free";
            var text = $" {String.Format(DataConstants.DataConstrain.seatNumber,Row,Column)} - {seatIsBooked}";
            return text;
        }
    }
}
