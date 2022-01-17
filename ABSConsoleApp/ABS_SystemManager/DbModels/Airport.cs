using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ABS_SystemManager.DbModels
{
    public partial class Airport
    {
        public Airport()
        {
            FlightDestination = new HashSet<Flight>();
            FlightOrigin = new HashSet<Flight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Flight> FlightDestination { get; set; }
        public virtual ICollection<Flight> FlightOrigin { get; set; }
    }
}
