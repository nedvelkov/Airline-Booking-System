namespace Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ABSComon;
    using Models.Contracts;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Airline:IAirline
    {
        private string name;
        private ICollection<IFlight> flights;

        public Airline(string name)
        {
            this.Name = name;
            this.flights = new List<IFlight>();
        }
        public string Name
        {
            get { return this.name; }
            init
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ||value.Length >= 6)
                {
                    throw new ArgumentException("Name of airline must be between 1 and 5 characters");
                }
                var regex = new Regex("^[a-zA-Z]{1,5}$");
                if (regex.IsMatch(value)==false)
                {
                    throw new ArgumentException("Name of airline must have only letters");

                }
                this.name = value;
            }
        }

        public IReadOnlyCollection<IFlight> Flights => this.flights.AsReadOnly();

        public void AddFlight(IFlight flight)
        {
            var getFlight = this.flights.FirstOrDefault(x => x.Id == flight.Id);
            if (getFlight != null)
            {
                throw new ArgumentException($"Flight with id:{flight.Id} already exist");
            }
            this.flights.Add(flight);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var hasFlights = this.Flights.Count > 0 ? $"offers flights to over {this.Flights.Count} destinations" : "is growing and gets new offers to all destinations";
            sb.AppendLine($"Airlne {this.Name} {hasFlights}");
            if (this.flights.Count > 0)
            {
            this.flights.ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }
    }
}
