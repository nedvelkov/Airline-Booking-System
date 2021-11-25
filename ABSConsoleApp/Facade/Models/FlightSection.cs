namespace Facade.Models
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Facade.Interfaces;

    class FlightSection : IFlightSection
    {
        private Dictionary<ISeatNumber, ISeat> _seats;

        public FlightSection() => _seats = new();
        public SeatClass SeatClass { get; init; }

        public IReadOnlyDictionary<ISeatNumber, ISeat> Seats => _seats;

        public bool HasAvaibleSeats() => this._seats.Any(x => x.Value.Booked == false);

        public void BookSeat(ISeatNumber number) => _seats[number].BookSeat();

        public void AddSeats(IEnumerable<ISeat> seats) => seats.ToList().ForEach(x => _seats.Add(x.Number, x));

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight section {SeatClass} class with {_seats.Count} seats");
            _seats.ToList().ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }

    }
}
