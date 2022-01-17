using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ABS_SystemManager.DbModels
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

        public virtual Airline Airline { get; set; }
        public virtual Airport Destination { get; set; }
        public virtual Airport Origin { get; set; }
        public virtual ICollection<FlightSection> FlightSection { get; set; }
    }
}
