using System;
using System.Collections.Generic;

namespace ABS_SystemManager.Data.DbModels
{
    public partial class Flight
    {
        public Flight()
        {
            FlightSection = new HashSet<FlightSection>();
        }

        public string Id { get; set; }
        public int AirlineId { get; set; }
        public int OriginId { get; set; }
        public int DestinationId { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeparted { get; set; }

        public virtual Airline Airline { get; set; }
        public virtual Airport Destination { get; set; }
        public virtual Airport Origin { get; set; }
        public virtual ICollection<FlightSection> FlightSection { get; set; }
    }
}
