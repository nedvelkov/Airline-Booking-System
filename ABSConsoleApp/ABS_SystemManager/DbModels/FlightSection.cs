using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ABS_SystemManager.DbModels
{
    public partial class FlightSection
    {
        public FlightSection()
        {
            Seat = new HashSet<Seat>();
        }

        public int Id { get; set; }
        public short SeatClass { get; set; }
        public string FlightId { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
