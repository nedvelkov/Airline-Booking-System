namespace Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Enums;

    public class Flight
    {
        private string id;
        private DateTime date;
        private Airport origin;
        private Airport destination;
        private List<FlightSection> flightSections;

        public Flight(Airport org, Airport dest, DateTime date, string id)
        {
            this.Origin = org;
            this.Destination = dest;
            this.Date = date;
            this.Id = id;
            this.flightSections = new List<FlightSection>();
        }
        public string Id
        {
            get { return this.id; }
            init { this.id = value; }
        }

        public DateTime Date
        {
            get { return this.date; }
            set
            {
                var today = DateTime.UtcNow.Date;
                if (DateTime.Compare(value.Date, today) < 0)
                {
                    throw new ArgumentException("Date is not valid.");
                }
                this.date = value;
            }
        }

        public Airport Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        public Airport Destination
        {
            get { return this.destination; }
            set
            {
                if (this.origin.Equals(value))
                {
                    throw new ArgumentException("Destionation must be different from origin.");
                }
                this.destination = value;
            }
        }

        public IReadOnlyCollection<FlightSection> FlightSections => this.flightSections;

        public void AddFlightSection(SeatClass seatClass, int rows, int colmns)
        {
            var getSection = this.flightSections.FirstOrDefault(x => x.SeatClass == seatClass);
            if (getSection != null)
            {
                throw new ArgumentException($"Flight already have a {seatClass} section");
            }
            var flightSection = new FlightSection(seatClass, rows, colmns);
            this.flightSections.Add(flightSection);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Flight #{this.Id} from ${this.Origin} to ${this.Destination}");
            sb.AppendLine($"The flight has {this.flightSections.Count} section:");
            this.flightSections.ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }
    }
}
