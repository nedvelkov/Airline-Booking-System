using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABS_WebApp.ViewModels;
using ABS_SystemManager.Interfaces;
using static ABS_SystemManager.DataConstants.Success;

namespace ABS_WebApp.Controllers
{
    public class AppController:Controller
    {
        private readonly ISystemManager _manager;

        public AppController(ISystemManager manager) => _manager = manager;

        [HttpGet]
        public async Task<IActionResult> CreateAirport() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAirport(CreateAirportViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _manager.CreateAirport(model.Name);
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
                ViewBag.Success = _manager.CreateAirline(model.Name);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFlight() => View();

        [HttpPost]
        public async Task<IActionResult> CreateFlight(CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _manager.CreateFlight(model.AirlineName,
                                                        model.Origin,
                                                        model.Destination,
                                                        model.Date.Year,
                                                        model.Date.Month,
                                                        model.Date.Day,
                                                        model.Id);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateSection() => View();

        [HttpPost]
        public async Task<IActionResult> CreateSection(CreateSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _manager.CreateSection(model.AirlineName,
                                                         model.Id,
                                                         model.Rows,
                                                         model.Columns,
                                                         model.SeatClass);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FindAvailableFlights() => View(new FindAvaibleFlightsViewModel());

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
        public async Task<IActionResult> BookSeat() => View();

        [HttpPost]
        public async Task<IActionResult> BookSeat(BookSeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Success = _manager.BookSeat(model.AirlineName,
                                                    model.Id,
                                                    model.SeatClass,
                                                    model.Row,
                                                    model.Column[0]);
                ModelState.Clear();
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails()
        {
            var details = _manager.DisplaySystemDetails().Split("\r\n").ToList();
            var model = new DisplaySystemDetailsViewModel();
            model.Details = details;
            return View(model);
        }
    }
}
