using System;
using System.Collections.Generic;

using ABS_WebAPI.ApiModels;
using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABS_WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService) => _flightService = flightService;

        [HttpGet]
        public IEnumerable<string> Get() => _flightService.GetFlights();

        [HttpGet]
        [Route("/api/aviableflights")]
        public string GetAviableFlights(AviableFlightsRequestModel flightsRequestModel)
            => _flightService.FindAvailableFlights(flightsRequestModel.Origin, flightsRequestModel.Destination);

        [HttpPost]
        public string Post(FlightRequestModel flight)
            => _flightService.CreateFlight(flight.AirlineName, flight.Origin, flight.Destination, flight.Date.Year, flight.Date.Month, flight.Date.Day, flight.Id);

    }
}
