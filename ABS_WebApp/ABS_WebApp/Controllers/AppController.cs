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
                _manager.CreateAirport(model.Name);
                ViewBag.Success = string.Format(createdAirport, model.Name);
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
                _manager.CreateAirline(model.Name);
                ViewBag.Success = string.Format(createdAirline, model.Name);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFlight() => View();

        [HttpGet]
        public async Task<IActionResult> CreateSection() => View();

        [HttpGet]
        public async Task<IActionResult> FindAvailableFlights() => View();

        [HttpGet]
        public async Task<IActionResult> BookSeat() => View();

        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails() => View();
    }
}
