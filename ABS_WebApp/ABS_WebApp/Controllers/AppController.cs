using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ABS_WebApp.ViewModels;
using ABS_WebApp.Services;
using ABS_WebApp.ViewModels.DisplayObjectModels;

using static ABS_DataConstants.DataConstrain;

namespace ABS_WebApp.Controllers
{
    [Authorize(Roles =USER_ROLE)]
    public class AppController : Controller
    {
        private readonly WebApiService _webApiService;

        public AppController(WebApiService webApiService) => _webApiService = webApiService;


        [HttpGet]
        public async Task<IActionResult> FindAvailableFlights() => View(await GetFindAvaibleFlightsViewModel());

        [HttpPost]
        public async Task<IActionResult> FindAvailableFlights(FindAvaibleFlightsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _webApiService.GetAviableFlights(model.Flight);

                ParseData(data, model);
            }
            var dataAirports = await _webApiService.GetAirports();
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
                TempData["Result"] = await _webApiService.BookSeat(model.Seat);
                ModelState.Clear();
            }
            return View(await GetBookSeatViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> DisplaySystemDetails()
        {
            var data = await _webApiService.GetSystemDetails();
            var model = new DisplaySystemDetailsViewModel();

            ParseData(data, model);
            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<FindAvaibleFlightsViewModel> GetFindAvaibleFlightsViewModel()
        {
            var model = new FindAvaibleFlightsViewModel();
            var dataAirports = await _webApiService.GetAirports();
            model.Airports = dataAirports.ToList();
            return model;
        }

        private async Task<BookSeatViewModel> GetBookSeatViewModel()
        {
            var model = new BookSeatViewModel();
            var dataAirlines = await _webApiService.GetAirlines();
            model.Airlines = dataAirlines.ToList();
            var dataFlights = await _webApiService.GetFlights();
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
