﻿using System;
using System.Collections.Generic;

using ABS_Models;
using ABS_WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static ABS_DataConstants.DataConstrain;

namespace ABS_WebAPI.Controllers
{
    [Route(FLIGHT_API_PATH)]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService) => _flightService = flightService;

        [HttpGet]
        public IEnumerable<string> Get() => _flightService.GetFlights();

        [HttpGet]
        [Route(FIND_FLIGHT_API_PATH)]
        public string GetAviableFlights(AviableFlightsModel flightsRequestModel)
            => _flightService.FindAvailableFlights(flightsRequestModel.Origin, flightsRequestModel.Destination);

        [HttpPost]
        public string Post(FlightModel flight)
            => _flightService.CreateFlight(flight.AirlineName, flight.Origin, flight.Destination, flight.DateOfFlight.Year, flight.DateOfFlight.Month, flight.DateOfFlight.Day, flight.Id);

    }
}
