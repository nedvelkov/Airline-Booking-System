using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using ABS_WebApp.ViewModels;
using ABS_WebApp.Services.Interfaces;
using System.Diagnostics;
using ABS_Models;
using ABS_WebApp.ViewModels.DisplayObjectModels;

namespace ABS_WebApp.Controllers
{
    public class AppController : Controller
    {
        private readonly IAirlineService _airlineService;
        private readonly IAirportService _airportService;
        private readonly IFlightService _flightService;
        private readonly ISystemService _systemService;

        public AppController(IAirlineService airlineService,
                             IAirportService airportService,
                             IFlightService flightService,
                             ISystemService systemService)
        {
            _airlineService = airlineService;
            _airportService = airportService;
            _flightService = flightService;
            _systemService = systemService;
        }

        [HttpGet]
        public IActionResult CreateAirport() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAirport(AirportModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _airportService.CreateAirport(model);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAirline() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAirline(AirlineModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _airlineService.CreateAirline(model);
                ModelState.Clear();
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateFlight() => View(await GetFlightModel());

        [HttpPost]
        public async Task<IActionResult> CreateFlight(CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _flightService.CreateFlight(model.Flight);
                ModelState.Clear();
            }
            return View(await GetFlightModel());
        }

        [HttpGet]
        public async Task<IActionResult> CreateSection() => View(await GetCreateSectionViewModel());

        [HttpPost]
        public async Task<IActionResult> CreateSection(CreateSectionViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _flightService.CreateFlightSection(model.FlightSection);
                ModelState.Clear();
            }
            return View(await GetCreateSectionViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> FindAvailableFlights() => View(await GetFindAvaibleFlightsViewModel());

        [HttpPost]
        public async Task<IActionResult> FindAvailableFlights(FindAvaibleFlightsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _flightService.FindAvailableFlights(model.Flight);

                ParseData(data, model);
            }
            var dataAirports = await _airportService.Airports();
            model.Airports = dataAirports.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> BookSeat() => View(await GetBookSeatViewModel());

        [HttpPost]
        public async Task<IActionResult> BookSeat(BookSeatViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["Result"] = await _flightService.BookSeat(model.Seat);
                ModelState.Clear();
            }
            return View(await GetBookSeatViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails()
        {
            var data = await _systemService.Details();
            var model = new DisplaySystemDetailsViewModel();

            ParseData(data, model);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

        private async Task<FindAvaibleFlightsViewModel> GetFindAvaibleFlightsViewModel()
        {
            var model = new FindAvaibleFlightsViewModel();
            var dataAirports = await _airportService.Airports();
            model.Airports = dataAirports.ToList();
            return model;
        }

        private async Task<BookSeatViewModel> GetBookSeatViewModel()
        {
            var model = new BookSeatViewModel();
            var dataAirlines = await _airlineService.Airlines();
            model.Airlines = dataAirlines.ToList();
            var dataFlights = await _flightService.Flights();
            model.Flights = dataFlights.ToList();
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

        private SeatViewModel[,] GetSeats(List<string> seats)
        {
            var lastSeat = seats.LastOrDefault();
            if (lastSeat == null)
            {
                return new SeatViewModel[0, 0];
            }
            var number = lastSeat.Split('-', StringSplitOptions.RemoveEmptyEntries).First().Trim();
            var rows = int.Parse(number.Substring(0, 3));
            var columns = (int)number[3] - 64;
            var seatArray = new SeatViewModel[rows, columns];
            var index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    var line = seats[index];
                    var seat = new SeatViewModel();
                    seat.Booked = line.EndsWith("booked");
                    seat.Number = line.Split('-', StringSplitOptions.RemoveEmptyEntries).First().Trim();
                    seatArray[row, col] = seat;
                    index++;
                }
            }
            return seatArray;
        }

        private void AddSeats(DisplaySystemDetailsViewModel model, List<string> seats)
        {
            var lastAirline = model.AirlinesList.LastOrDefault();
            if (lastAirline == null)
            {
                return;
            }
            var lastFlight = lastAirline.Flights.LastOrDefault();
            if (lastFlight == null)
            {
                return;
            }
            var flightSection = lastFlight.FlightSections.LastOrDefault();
            if (flightSection != null)
            {
                flightSection.Seats = GetSeats(seats);
                seats.Clear();
            }
        }

        private void AddSeats(List<FlightViewModel> flights, List<string> seats)
        {
            var lastFlight = flights.LastOrDefault();
            if (lastFlight == null)
            {
                return;
            }
            var flightSection = lastFlight.FlightSections.LastOrDefault();
            if (flightSection != null)
            {
                flightSection.Seats = GetSeats(seats);
                seats.Clear();
            }
        }

        private void ParseData(string data, DisplaySystemDetailsViewModel model)
        {
            var details = data.Split("\r\n").ToList();
            var listSeats = new List<string>();
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
                        AddSeats(model, listSeats);
                        var airline = new AirlineViewModel();
                        airline.Title = line.Remove(0, 3);
                        model.AirlinesList.Add(airline);
                        break;
                    case "-FL":
                        AddSeats(model, listSeats);
                        var flight = new FlightViewModel();
                        flight.Title = line.Remove(0, 3);
                        var lastAirline = model.AirlinesList.Last();
                        lastAirline.Flights.Add(flight);
                        break;
                    case "-FS":
                        AddSeats(model, listSeats);
                        var flightSection = new FlightSectionViewModel();
                        flightSection.Title = line.Remove(0, 3);
                        var lastFlight = model.AirlinesList.Last().Flights.Last();
                        lastFlight.FlightSections.Add(flightSection);
                        break;
                    default:
                        listSeats.Add(line);
                        break;
                }
            }
            AddSeats(model, listSeats);
        }

        private void ParseData(string data, FindAvaibleFlightsViewModel model)
        {
            var details = data.Split("\r\n").ToList();

            if (details.First().Contains("-FL"))
            {
                var listSeats = new List<string>();
                foreach (string line in details)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    var code = line.Substring(0, 3);
                    switch (code)
                    {
                        case "-FL":
                            AddSeats(model.Flights, listSeats);
                            var flight = new FlightViewModel();
                            flight.Title = line.Remove(0, 3);
                            model.Flights.Add(flight);
                            break;
                        case "-FS":
                            AddSeats(model.Flights, listSeats);
                            var flightSection = new FlightSectionViewModel();
                            flightSection.Title = line.Remove(0, 3);
                            var lastFlight = model.Flights.Last();
                            lastFlight.FlightSections.Add(flightSection);
                            break;
                        default:
                            listSeats.Add(line);
                            break;
                    }
                }
            }
            else
            {
                model.Error = details.First();
            }
        }

    }
}
