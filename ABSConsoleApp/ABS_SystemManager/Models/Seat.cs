using System;

using ABS_SystemManager.Interfaces;
using static ABS_DataConstants.DataConstrain;

namespace ABS_SystemManager.Models
{
    internal class Seat : ISeat
    {
        private bool _booked;

        public int Row { get; set; }
        public char Column { get; set; }

        public bool Booked => _booked;

        public void BookSeat() => _booked = true;

        public override string ToString()
        {
            var seatIsBooked = Booked ? "booked" : "free";
            var text = $" {String.Format(seatNumber, Row, Column)} - {seatIsBooked}";
            return text;
        }
    }
}
