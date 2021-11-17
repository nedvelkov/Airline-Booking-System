namespace Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Airline
    {
        private string name;
        private List<Flight> flights;

        public Airline(string name)
        {
            this.Name = name;
            this.flights = new List<Flight>();
        }
        public string Name
        {
            get { return this.name; }
            init
            {
                if (string.IsNullOrEmpty(value) || value.Length >= 6)
                {
                    throw new ArgumentException("Name of airline must be between 1 and 5 characters");
                }
                this.name = value;
            }
        }

        public IReadOnlyList<Flight> Flights => this.flights;

        public void AddFlight(Flight flight)
        {
            var getFlight = this.flights.FirstOrDefault(x => x.Id == flight.Id);
            if (getFlight != null)
            {
                throw new ArgumentException($"Flight with id:{flight.Id} already exist");
            }
            this.flights.Add(flight);
        }

    }
}
