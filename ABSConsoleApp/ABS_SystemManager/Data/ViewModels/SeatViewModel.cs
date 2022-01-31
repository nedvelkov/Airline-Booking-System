using System;

using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.Data.ViewModels
{
    public class SeatViewModel
    {
        public int Row { get; set; }
        public char Column { get; set; }

        public bool Booked { get; set; }

        public override string ToString()
        {
            var seatIsBooked = Booked ? "booked" : "free";
            var text = $" {String.Format(SEAT_NUMBER_TO_STRING, Row, Column)} - {seatIsBooked}";
            return text;
        }
    }
}
