using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ABS_SystemManager.DbModels
{
    public partial class Seat
    {
        public long Id { get; set; }
        public short Row { get; set; }
        public string Column { get; set; }
        public int FlightSectionId { get; set; }

        public virtual FlightSection FlightSection { get; set; }
    }
}
