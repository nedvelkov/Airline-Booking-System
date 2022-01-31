namespace ABS_SystemManager.Data.DbModels
{
    public partial class Seat
    {
        public long Id { get; set; }
        public short Row { get; set; }
        public string Column { get; set; }
        public int FlightSectionId { get; set; }
        public bool Booked { get; set; }

        public virtual FlightSection FlightSection { get; set; }
    }
}
