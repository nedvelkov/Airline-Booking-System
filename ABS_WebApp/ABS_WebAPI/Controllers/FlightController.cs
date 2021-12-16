using System.Collections.Generic;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABS_WebAPI.Controllers
{
    [ApiController]
    public class FlightController:ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService) => _flightService = flightService;

        [HttpGet]
        [Route("/api/[controller]")]
        public IEnumerable<string> Get() => _flightService.GetFlights();

        [HttpGet]
        [Route("/api/aviableflights")]
        public string GetAviableFlights(AviableFlightsRequestModel flightsRequestModel) 
            => _flightService.FindAvailableFlights(flightsRequestModel.Origin, flightsRequestModel.Destination);

        [HttpPost]
        [Route("/api/{airlinename}/[controller]")]
        public string Post(string airlineName, FlightRequestModel flight) 
            => _flightService.CreateFlight(airlineName, flight.Origin, flight.Destination, flight.Date.Year, flight.Date.Month, flight.Date.Day, flight.Id);
    }
}
