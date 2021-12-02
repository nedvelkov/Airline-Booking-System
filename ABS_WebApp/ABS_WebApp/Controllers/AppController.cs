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
        public async Task<IActionResult> FindAvailableFlights() => View();

        [HttpGet]
        public async Task<IActionResult> BookSeat() => View();

        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails() => View();
    }
}
