namespace Facade.Models
{
    using Facade.Interfaces;

    class Seat:ISeat
    {
        private ISeatNumber _number;
        private bool _booked;

        public Seat(ISeatNumber number) => _number = number;

        public ISeatNumber Number => _number;

        public bool Booked => _booked;

        public void BookSeat() => _booked = true;

        public override string ToString()
        {
            var seatIsBooked = this.Booked ? "booked" : "free";
            var text = $" {_number.Row.ToString("D3")}{_number.Colmn} - {seatIsBooked}";
            return text;
        }
    }
}
