using System;

using static ABS_SystemManager.DataConstants.SystemDataConstrain;

namespace ABS_SystemManager.DbModels
{
    public class AvailableFlights
    {
        public string Id { get; set; }

        public string AirlineName { get; set; }

        public DateTime Date { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public override string ToString() 
            => string.Format(FLIGHT_TO_STRING_TITLE_DATABASE, Id, Origin, Destination, AirlineName, Date.ToString(FORMAT_FOR_DATE_TIME));
    }
}
