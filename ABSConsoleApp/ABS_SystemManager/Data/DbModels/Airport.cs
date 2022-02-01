using System.Collections.Generic;

namespace ABS_SystemManager.Data.DbModels
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
