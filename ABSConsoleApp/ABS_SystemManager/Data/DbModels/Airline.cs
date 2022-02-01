using System.Collections.Generic;

namespace ABS_SystemManager.Data.DbModels
{
    public partial class Airline
    {
        public Airline() => Flight = new HashSet<Flight>();

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Flight> Flight { get; set; }
    }
}
