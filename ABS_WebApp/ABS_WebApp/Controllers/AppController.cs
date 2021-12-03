﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABS_WebApp.ViewModels;
using ABS_SystemManager.Interfaces;
using ABS_WebApp.Seeder;
using static ABS_SystemManager.DataConstants.Success;

namespace ABS_WebApp.Controllers
{
    public class AppController : Controller
    {
        private readonly ISystemManager _manager;

        public AppController(ISystemManager manager)
        {
            _manager = manager;
            var seeder = new DataSeeder(_manager);
            seeder.Seed();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAirport() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAirport(CreateAirportViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = _manager.CreateAirport(model.Name);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAirline() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAirline(CreateAirlineViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = _manager.CreateAirline(model.Name);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFlight() => View(GetFlightModel());

        [HttpPost]
        public async Task<IActionResult> CreateFlight(CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = _manager.CreateFlight(model.AirlineName,
                                                        model.Origin,
                                                        model.Destination,
                                                        model.Date.Year,
                                                        model.Date.Month,
                                                        model.Date.Day,
                                                        model.Id);
                ModelState.Clear();
            }
            return View(GetFlightModel());
        }

        [HttpGet]
        public async Task<IActionResult> CreateSection() => View(GetCreateSectionViewModel());

        [HttpPost]
        public async Task<IActionResult> CreateSection(CreateSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = _manager.CreateSection(model.AirlineName,
                                                         model.Id,
                                                         model.Rows,
                                                         model.Columns,
                                                         model.SeatClass);
                ModelState.Clear();
            }
            return View(GetCreateSectionViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> FindAvailableFlights() => View(GetFindAvaibleFlightsViewModel());

        [HttpPost]
        public async Task<IActionResult> FindAvailableFlights(FindAvaibleFlightsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Flights = _manager.FindAvailableFlights(model.Origin, model.Destination).Split("\r\n").ToList();
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BookSeat() => View(GetBookSeatViewModel());

        [HttpPost]
        public async Task<IActionResult> BookSeat(BookSeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = _manager.BookSeat(model.AirlineName,
                                                    model.Id,
                                                    model.SeatClass,
                                                    model.Row,
                                                    model.Column[0]);
                ModelState.Clear();
            }
            return View(GetBookSeatViewModel());
        }


        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails()
        {
            var details = _manager.DisplaySystemDetails().Split("\r\n").ToList();
            var model = new DisplaySystemDetailsViewModel();
            foreach (string line in details)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var code = line.Substring(0, 3);
                switch (code)
                {
                    case "=AP":
                        model.AirportsTitle = line.Remove(0, 3);
                        break;
                    case "-AP":
                        model.AirporstList.Add(line.Remove(0, 3));
                        break;
                    case "=AL":
                        model.AirlinesTitle = line.Remove(0, 3);
                        break;
                    case "-AL":
                        var airline = new AirlineViewModel();
                        airline.Title = line.Remove(0, 3);
                        model.AirlinesList.Add(airline);
                        break;
                    case "-FL":
                        var flight = new FlightViewModel();
                        flight.Title = line.Remove(0, 3);
                        var lastAirline = model.AirlinesList.Last();
                        lastAirline.Flights.Add(flight);
                        break;
                    case "-FS":
                        var flightSection = new FlightSectionViewModel();
                        flightSection.Title = line.Remove(0, 3);
                        var lastFlight = model.AirlinesList.Last().Flights.Last();
                        lastFlight.FlightSections.Add(flightSection);
                        break;
                    default:
                        var seat = new SeatViewModel();
                        seat.Booked = line.EndsWith("booked");
                        seat.Number = line.Split('-', StringSplitOptions.RemoveEmptyEntries).First().Trim();
                        var lastSection = model.AirlinesList.Last().Flights.Last().FlightSections.Last();
                        lastSection.Seats.Add(seat);
                        break;
                }
            }
            model.Details = details;
            return View(model);
        }

        private CreateFlightViewModel GetFlightModel()
        {
            var model = new CreateFlightViewModel();
            model.Airlines = _manager.ListAirlines.ToList();
            model.Airports = _manager.ListAirports.ToList();
            model.Date = DateTime.Now;
            model.Id = null;
            return model;
        }

        private FindAvaibleFlightsViewModel GetFindAvaibleFlightsViewModel()
        {
            var model = new FindAvaibleFlightsViewModel();
            model.Airports = _manager.ListAirports.ToList();
            return model;
        }

        private BookSeatViewModel GetBookSeatViewModel()
        {
            var model = new BookSeatViewModel();
            model.Airlines = _manager.ListAirlines.ToList();
            model.Flights = _manager.ListFlights.ToList();
            return model;
        }

        private CreateSectionViewModel GetCreateSectionViewModel()
        {
            var model = new CreateSectionViewModel();
            model.Airlines = _manager.ListAirlines.ToList();
            model.Flights = _manager.ListFlights.ToList();
            return model;
        }
    }
}
