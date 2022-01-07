using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ABS_Models;
using ABS_WebApp.ViewModels;
using ABS_WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles =ADMIN_ROLE)]
    [Area("Admin")]
    public class CreateController:Controller
    {
        private readonly IAirlineService _airlineService;
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;

        public CreateController(IAirlineService airlineService,
                             IAirportService airportService,
                             IFlightService flightService)
        {
            _airlineService = airlineService;
            _airportService = airportService;
            _flightService = flightService;
        }


        [HttpGet]
        public IActionResult Airport() => View();

        [HttpPost]
        public async Task<IActionResult> Airport(AirportModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _airportService.CreateAirport(model);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Airline() => View();

        [HttpPost]
        public async Task<IActionResult> Airline(AirlineModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _airlineService.CreateAirline(model);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Flight() => View(await GetFlightModel());

        [HttpPost]
        public async Task<IActionResult> Flight(CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _flightService.CreateFlight(model.Flight);
                ModelState.Clear();
            }
            return View(await GetFlightModel());
        }

        [HttpGet]
        public async Task<IActionResult> Section() => View(await GetCreateSectionViewModel());

        [HttpPost]
        public async Task<IActionResult> Section(CreateSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _flightService.CreateFlightSection(model.FlightSection);
                ModelState.Clear();
            }
            return View(await GetCreateSectionViewModel());
        }


        private async Task<CreateFlightViewModel> GetFlightModel()
        {
            var model = new CreateFlightViewModel();
            var dataAirlines = await _airlineService.Airlines();
            model.Airlines = dataAirlines.ToList();
            var dataAirports = await _airportService.Airports();
            model.Airports = dataAirports.ToList();
            model.Flight.DateOfFlight = DateTime.Now;
            model.Flight.Id = string.Empty;
            return model;
        }

        private async Task<CreateSectionViewModel> GetCreateSectionViewModel()
        {
            var model = new CreateSectionViewModel();
            var dataAirlines = await _airlineService.Airlines();
            model.Airlines = dataAirlines.ToList();
            var dataFlights = await _flightService.Flights();
            model.Flights = dataFlights.ToList();
            return model;
        }

    }
}
