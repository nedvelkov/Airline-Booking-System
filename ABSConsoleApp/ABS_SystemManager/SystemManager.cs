using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ABS_SystemManager.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


using ABS_SystemManager.Models;
using ABS_SystemManager.Interfaces;
using static ABS_DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Success;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;
using static ABS_SystemManager.DataConstants.SystemError;
using static ABS_DataConstants.Error;
using System.Threading.Tasks;

namespace ABS_SystemManager
{
    public class SystemManager : ISystemManager
    {
        private Dictionary<string, IAirline> _airlines;
        private readonly ABS_databaseContext _databaseContext;

        private DateTime _testDate;

        public SystemManager(ABS_databaseContext context)
        {
            _airlines = new Dictionary<string, IAirline>();
            _databaseContext = context;
        }

        public async Task<string> CreateAirport(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);

                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateAirport {name}");
            }
            catch (Exception a)
            {

                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_AIRPORT, name);
        }

        public async Task<string> CreateAirline(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);
                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateAirline {name}");
            }
            catch (Exception a)
            {

                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_AIRLINE, name);
        }

        public async Task<string> CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {

            try
            {
                ValidateFlightDate(new DateTime(year, month, day), INVALID_DATE_OF_DEPARTURE_FLIGHT);
                ValidateString(id, EVALUATE_FLIGHT_ID, FLIGHT_TOOLTIP);

                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateFlight {airlineName},{origin},{destination},{year},{month},{day},{id}");
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_FLIGHT, origin, destination, airlineName);
        }

        public async Task<string> CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass)
        {
            try
            {

                ValidateCountOfSeats(rows, "Rows", MIN_SEAT_ROWS, MAX_SEAT_ROWS);
                ValidateCountOfSeats(columns, "Columns", MIN_SEAT_COLUMNS, MAX_SEAT_COLUMNS);
                ValidateEnum<SeatClass>(seatClass);

                ValidateString(airlineName, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);
                ValidateString(flightId, EVALUATE_FLIGHT_ID, FLIGHT_TOOLTIP);

                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateFlightSection {airlineName},{flightId},{rows},{columns},{seatClass}");
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_FLIGHT_SECTION, (SeatClass)seatClass, flightId, airlineName);
        }

        public string FindAvailableFlights(string origin, string destination)
        {
            List<AvailableFlights> flights;
            try
            {
                 ValidateFlightDestination(origin, destination);
                flights= _databaseContext.GetAvailableFlights.FromSqlRaw($"usp_FindAvailableFlights {origin},{destination}").ToList();
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return flights.Count > 0 
                                 ? string.Concat(Environment.NewLine,flights) 
                                 : string.Format(NO_AVIABLE_FLIGHTS, origin, destination);

        }

        public async Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
        {
            try
            {
                ValidateString(flightId, EVALUATE_FLIGHT_ID, FLIGHT_TOOLTIP);
                ValidateString(airlineName, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);

                var lastSeatNumber = _databaseContext.GetSeatNumbers.FromSqlRaw($"sp_GetRowsAndColumsOfFlightSection {flightId},{seatClass}").FirstOrDefault();
                CheckIfSeatIsValid(row, column, lastSeatNumber);

                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_BookSeat {airlineName},{flightId},{seatClass},{row},{column}");
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return string.Format(SUCCESSFUL_BOOKED_SEAT, string.Format(SEAT_NUMBER_TO_STRING, row, column), (SeatClass)seatClass, flightId, airlineName);

        }

        public string DisplaySystemDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine(DISPLAY_AIRPORTS_TITLE);

            _databaseContext.GetNames.FromSqlRaw("usp_GetAirlineNames")
                                        .Select(x => string.Format(AIRPORT_TO_STRING_TITLE, x.Name))
                                        .ToList()
                                        .ForEach(x => sb.AppendLine(x.ToString()));
            
            sb.AppendLine(string.Format(DISPLAY_AIRLINES_TITLE, _airlines.Count));
            if (_airlines.Count > 0)
            {
                _airlines.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }

        public IReadOnlyList<string> ListAirlines => _databaseContext.GetNames.FromSqlRaw("usp_GetAirlineNames").Select(x => x.Name).ToList();

        public IReadOnlyList<string> ListAirports => _databaseContext.GetNames.FromSqlRaw("usp_GetAirportNames").Select(x => x.Name).ToList();

        public IReadOnlyList<string> ListFlights => _databaseContext.GetIds.FromSqlRaw("usp_GetFlightIds").Select(x => x.Id).ToList();

        /// <summary>
        /// Validate <paramref name="text"/> based on regex <paramref name="expression"/>.
        /// Throw error with <paramref name="message"/> if <paramref name="text"/> is not valid.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ValidateString(string text, string expression, string message)
        {
            var regex = new Regex(expression);
            if (string.IsNullOrEmpty(text) || !regex.IsMatch(text))
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Validate if <paramref name="origin"/> point of flight is different from <paramref name="destination"/>.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="message"></param>
        private void ValidateFlightDestination(string origin, string destination)
        {
            ValidateString(origin, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);
            ValidateString(destination, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);
            if (origin == destination)
            {
                throw new ArgumentException(WRONG_DESTINATION);
            }
        }

        /// <summary>
        /// Check if <paramref name="date"/> is in past.Throw error with <paramref name="message"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        private void ValidateFlightDate(DateTime date, string message)
        {
            var dateToCompare = _testDate != DateTime.MinValue ? _testDate : DateTime.Now.Date;
            if (DateTime.Compare(date.Date, dateToCompare) <= 0)
            {
                throw new ArgumentException(message);
            }
        }

        private void CheckIfSeatIsValid(int row, char column, SeatNumber seatNumber)
        {
            if (seatNumber == null)
            {
                throw new ArgumentException(INVALID_SEAT_CLASS);

            }
            if (row < 0 || row >= seatNumber.Row)
            {
                throw new ArgumentException(INVALID_SEAT_ROW);
            }

            if (column < seatNumber.Column[0] || column > seatNumber.Column[0])
            {
                throw new ArgumentException(INVALID_SEAT_COLUMN);
            }
        }

        private T ValidateEnum<T>(object value)
        {
            var seatClass = (T)value;
            return Enum.IsDefined(typeof(T), value) ? seatClass : throw new ArgumentException("Seat class is not valid");
        }

        private void ValidateCountOfSeats(int value, string @type, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(string.Format(INVALID_SEAT_COUNT, type, min, max));
            }
        }

    }
}
