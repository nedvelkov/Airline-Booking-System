using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using ABS_SystemManager.Interfaces;
using ABS_SystemManager.Data.ViewModels;
using ABS_SystemManager.Data.UserDefineModels;

namespace ABS_SystemManager.Data
{
    public class AbsRepository : IAbsRepository
    {
        private readonly ABS_databaseContext _context;

        public AbsRepository(ABS_databaseContext context) => _context = context;

        public async Task<bool> CreateAirport(string name)
            => await _context.Database.ExecuteSqlInterpolatedAsync($"usp_CreateAirport {name}") == 1;

        public async Task<bool> HasAirport(string name)
        {
            var parameterReturn = new SqlParameter
            {
                ParameterName = "@HasAirport",
                SqlDbType = SqlDbType.Bit,
                Direction = ParameterDirection.Output,
            };
            var result = await _context.Database
                                       .ExecuteSqlRawAsync($"usp_HasAirport {name},@HasAirport OUTPUT", parameterReturn);

            return (bool)parameterReturn.Value;
        }

        public async Task<bool> CreateAirline(string name)
            => await _context.Database.ExecuteSqlInterpolatedAsync($"usp_CreateAirline {name}") == 1;

        public async Task<bool> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
            => await _context.Database
                             .ExecuteSqlInterpolatedAsync($"usp_CreateFlight {airlineName},{origin},{destination},{year},{month},{day},{id}") == 1;

        public async Task<bool> CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass)
            => await _context.Database
                             .ExecuteSqlInterpolatedAsync($"usp_CreateFlightSection {airlineName},{flightId},{rows},{columns},{seatClass}") == 1;

        public async Task<List<AvailableFlights>> FindAvailableFlights(string origin, string destination)
            => await _context.GetAvailableFlights
                       .FromSqlInterpolated($"usp_FindAvailableFlights {origin},{destination}")
                       .ToListAsync();

        public async Task<bool> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
            => await _context.Database
                             .ExecuteSqlInterpolatedAsync($"usp_BookSeat {airlineName},{flightId},{seatClass},{row},{column}") == 1;

        public async Task<List<AirlineViewModel>> GetAirlineViews()
        {
            var rowData = await _context.GetAirlineTableView.FromSqlRaw($"usp_GetArilinesView").ToListAsync();
            return ParseAirlineView(rowData);
        }

        public async Task<SeatNumber> GetLastSeatNumber(string flightId, int seatClass)
        {
            var list = await _context.GetSeatNumbers
                                       .FromSqlInterpolated($"usp_GetRowsAndColumsOfFlightSection {flightId},{seatClass}")
                                       .ToListAsync();
            return list.FirstOrDefault();
        }

        public List<string> ListAirlines => _context.GetNames
                                                    .FromSqlRaw("usp_GetAirlineNames")
                                                    .Select(x => x.Name).ToList();

        public List<string> ListAirports => _context.GetNames
                                                    .FromSqlRaw("usp_GetAirportNames")
                                                    .Select(x => x.Name).ToList()
                                                    .Select(x => x.Trim()).ToList();

        public List<string> ListFlights => _context.GetIds
                                                   .FromSqlRaw("usp_GetFlightIds")
                                                   .Select(x => x.Id).ToList()
                                                   .Select(x => x.Trim()).ToList();

        private List<AirlineViewModel> ParseAirlineView(List<AirlineTableView> airlineViews)
        {
            var airlines = new Dictionary<string, AirlineViewModel>();
            foreach (var row in airlineViews)
            {
                var airlineName = row.AirlineName;
                var flightId = row.FlightId;
                var seatClass = row.SeatClass;
                if (airlines.ContainsKey(airlineName))
                {
                    var currentAirline = airlines[airlineName];
                    var lastFlight = currentAirline.Flights.LastOrDefault() == null ? AddNewFlight(currentAirline, flightId, row.Origin, row.Destination) : currentAirline.Flights.Last();
                    if (lastFlight.FlightId != flightId)
                    {
                        lastFlight = AddNewFlight(airlines[airlineName], flightId, row.Origin, row.Destination);

                    }
                    if (AddNewFligtSection(lastFlight, seatClass) == false)
                    {
                        continue;
                    }
                    AddSeatToFlightSection(lastFlight.FlightSections.Last(), (int)row.Row, row.Column[0], (bool)row.Booked);
                }
                else
                {
                    var airline = new AirlineViewModel() { AirlineName = airlineName };
                    airlines.Add(airlineName, airline);
                    var newFlight = AddNewFlight(airline, flightId, row.Origin, row.Destination);
                    if (AddNewFligtSection(newFlight, seatClass) == false)
                    {
                        continue;
                    }
                    AddSeatToFlightSection(newFlight.FlightSections.Last(), (int)row.Row, row.Column[0], (bool)row.Booked);
                }

            }
            return airlines.Select(x => x.Value).ToList();
        }

        private void AddSeatToFlightSection(FlightSectionViewModel flightSection, int row, char column, bool booked)
        {
            var seat = new SeatViewModel
            {
                Row = row,
                Column = column,
                Booked = booked
            };
            flightSection.Seats.Add(seat);
        }

        private bool AddNewFligtSection(FlightViewModel flight, short? seatClass)
        {
            if (seatClass == null)
            {
                return false;
            }
            var lastSection = flight.FlightSections.LastOrDefault();
            var currentRowSeatClass = ValidateEnum<SeatClass>((int)seatClass);
            if (lastSection != null && currentRowSeatClass == lastSection.SeatClass)
            {
                return true;
            }
            var newFlightSection = new FlightSectionViewModel { SeatClass = currentRowSeatClass };
            flight.FlightSections.Add(newFlightSection);
            return true;
        }

        private FlightViewModel AddNewFlight(AirlineViewModel airline, string flightId, string origin, string destination)
        {
            var flight = new FlightViewModel { FlightId = flightId, Origin = origin, Destination = destination };
            airline.Flights.Add(flight);
            return flight;
        }

        private T ValidateEnum<T>(object value)
        {
            var seatClass = (T)value;
            return Enum.IsDefined(typeof(T), value) ? seatClass : throw new ArgumentException("Seat class is not valid");
        }
    }
}
