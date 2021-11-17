namespace Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Enums;
    public class FlightSection
    {
        private SeatClass seatClass;
        private List<Seat> seats;

        public FlightSection(SeatClass seatClass, int rows, int colmns)
        {
            this.SeatClass = seatClass;
            this.seats = new List<Seat>();
            this.AddSeats(rows, colmns);
        }
        public SeatClass SeatClass
        {
            get { return this.seatClass; }
            init
            {
                var flag = Enum.IsDefined(typeof(SeatClass), value);

                if (flag==false)
                {
                    throw new InvalidCastException("Seat class is not valid.");
                }
                this.seatClass = value;
            }
        }

        public IReadOnlyList<Seat> Seats => this.seats;

        private void AddSeats(int rows, int colms)
        {
            ValidateValues(rows, "Rows", 1, 100);
            ValidateValues(colms, "Columns", 1, 10);
            for (int row = 1; row <= rows; row++)
            {
                for (int colmn = 1; colmn <= colms; colmn++)
                {
                    this.AddSeat(row, colmn);
                }
            }
        }

        public bool HasAvaibleSeats() => this.seats.Any(x => x.Booked == false);

        public void BookSeat(int row, char colmn)
        {
            var seat = this.seats.FirstOrDefault(x => x.Row == row && x.Colmn == colmn);
            if (seat == null)
            {
                throw new NullReferenceException("Seat with this number doesn't exist.");
            }
            seat.BookSeat();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight section {this.seatClass} class with {this.seats.Count} seats");
            this.seats.ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }

        private void AddSeat(int row, int colmn)
        {
            var colmAsChar = (char)(colmn + 64);
            var seat = new Seat(row, colmAsChar);
            this.seats.Add(seat);
        }

        private void ValidateValues(int value,string @type,int min,int max)
        {
            if (value<min || value > max)
            {
                throw new ArgumentOutOfRangeException($"{type} of seat must be between {min} and {max}");
            }
        }
    }
}
