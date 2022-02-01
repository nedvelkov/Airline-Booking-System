using System.Collections.Generic;

namespace ABS_SystemManager.Data.DbModels
{
    public partial class FlightSection
    {
        public FlightSection() => Seat = new HashSet<Seat>();

        public int Id { get; set; }
        public short SeatClass { get; set; }
        public string FlightId { get; set; }

        public virtual Flight Flight { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
