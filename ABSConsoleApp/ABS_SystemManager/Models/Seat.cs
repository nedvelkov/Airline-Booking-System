namespace ABS_SystemManager.Models
{
    using ABS_SystemManager.Interfaces;

    class Seat:ISeat
    {
        private bool _booked;

        public ISeatNumber Number { get; init; }

        public bool Booked => _booked;

        public void BookSeat() => _booked = true;

        public override string ToString()
        {
            var seatIsBooked = Booked ? "booked" : "free";
            var text = $" {Number} - {seatIsBooked}";
            return text;
        }
    }
}
