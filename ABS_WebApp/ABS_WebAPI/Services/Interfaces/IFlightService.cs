﻿using System.Collections.Generic;

namespace ABS_WebAPI.Services.Interfaces
{
   public interface IFlightService
    {
        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id);

        public string FindAvailableFlights(string origin, string destination);

        public IEnumerable<string> GetFlights();
    }
}
