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
        private Dictionary<string, IAirport> _airports;
        private Dictionary<string, IFlight> _flights;
        private readonly ABS_databaseContext _databaseContext;


        private DateTime _testDate;

        public SystemManager()
        {
            _airlines = new Dictionary<string, IAirline>();
            _airports = new Dictionary<string, IAirport>();
            _flights = new Dictionary<string, IFlight>();
            _databaseContext = new ABS_databaseContext();
        }

        public async Task<string> CreateAirport(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);

               await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateAirport {name}");
               // var result = _databaseContext.Airport.FromSqlRaw($"usp_GetAirports").ToList();
            }
            catch (Exception a)
            {

                return a.Message;
            }

            return String.Format(SUCCESSFUL_CREATED_AIRPORT, name);
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

            return String.Format(SUCCESSFUL_CREATED_AIRLINE, name);
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

            return String.Format(SUCCESSFUL_CREATED_FLIGHT, origin, destination, airlineName);
        }

        public async Task<string> CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass)
        {
            try
            {

                ValidateCountOfSeats(rows, "Rows", MIN_SEAT_ROWS, MAX_SEAT_ROWS);
                ValidateCountOfSeats(columns, "Columns", MIN_SEAT_COLUMNS, MAX_SEAT_COLUMNS);
                GetEnum<SeatClass>(seatClass);

                ValidateString(airlineName, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);
                ValidateString(flightId, EVALUATE_FLIGHT_ID, FLIGHT_TOOLTIP);

                await _databaseContext.Database.ExecuteSqlRawAsync($"usp_CreateFlightSection {airlineName},{flightId},{rows},{columns},{seatClass}");
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return String.Format(SUCCESSFUL_CREATED_FLIGHT_SECTION, (SeatClass)seatClass, flightId, airlineName);
        }

        public string FindAvailableFlights(string origin, string destination)
        {
            IAirport originAirport;
            IAirport destinationAirport;
            try
            {
                (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            var sb = new StringBuilder();

            var flightIds = originAirport.DeparturesFlights().Intersect(destinationAirport.ArrivalFlights()).ToList();

            var flights = _flights.Where(x => flightIds.Contains(x.Key)
                                            && DateTime.Compare(x.Value.Date, DateTime.Now.Date) > 0)
                                            .Select(x => x.Value)
                                            .ToList();
            flights.ForEach(f => sb.AppendLine(f.ToString()));

            return flights.Count > 0 ? sb.ToString() : String.Format(NO_AVIABLE_FLIGHTS, origin, destination);

        }

        #region Methods for test

        /// <summary>
        /// This method is only for test purpose.
        /// </summary>
        /// <param name="sectionDate"></param>
        /// <returns></returns>
        public void SetTestDate(DateTime sectionDate)
        {
            _testDate = sectionDate.Date;
        }

        /// <summary>
        /// This method is for test purpose.It works with _testDate
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public string FindAvailableFlightsTest(string origin, string destination)
        {
            IAirport originAirport;
            IAirport destinationAirport;

            try
            {
                (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            var sb = new StringBuilder();

            var flightIds = originAirport.DeparturesFlights().Intersect(destinationAirport.ArrivalFlights()).ToList();

            var flights = _flights.Where(x => flightIds.Contains(x.Key)
                                            && DateTime.Compare(x.Value.Date, _testDate.Date) > 0)
                                            .Select(x => x.Value)
                                            .ToList();
            flights.ForEach(f => sb.AppendLine(f.ToString()));

            return flights.Count > 0 ? sb.ToString() : String.Format(NO_AVIABLE_FLIGHTS, origin, destination);
        }
        #endregion

        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
        {
            IFlight flight;
            IFlightSection flightSection;

            int arrayRow;
            int arrayColumn;

            try
            {
                flight = AirlaneGotFlight(flightId, airlineName);

                flightSection = GetFlightSection(flight, seatClass);
                arrayRow = row - 1;
                arrayColumn = (int)column - INITIAL_VALUE_FOR_SEAT_COLUMN_CHAR;

                CheckIfSeatIsValid(arrayRow, arrayColumn, flightSection);

                ChekIfSeatIsBooked(arrayRow, arrayColumn, flightSection);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            flightSection.BookSeat(arrayRow, arrayColumn);

            return String.Format(SUCCESSFUL_BOOKED_SEAT, String.Format(SEAT_NUMBER_TO_STRING, row, column), (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);

        }

        public string DisplaySystemDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(DISPLAY_AIRPORTS_TITLE, _airports.Count));
            if (_airports.Count > 0)
            {
                _airports.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }
            sb.AppendLine(String.Format(DISPLAY_AIRLINES_TITLE, _airlines.Count));
            if (_airlines.Count > 0)
            {
                _airlines.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }

        public IReadOnlyList<string> ListAirlines => _airlines.Select(x => x.Key).ToList();

        public IReadOnlyDictionary<string, string> AirlinesDictionary => _airlines.ToDictionary(x => x.Key, x => x.Value.ToString());

        public IReadOnlyList<string> ListAirports => _airports.Select(x => x.Key).ToList();

        public IReadOnlyList<string> ListFlights => _flights.Select(x => x.Key).ToList();

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
        /// Return item of type <typeparamref name="TValue"/> with key <paramref name="key"/>. 
        /// If <paramref name="dictionary"/> don't contains item with such <paramref name="key"/>, thrown erro with <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private TValue GetItem<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, string message)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new ArgumentException(message);
            }
            return dictionary[key];
        }

        /// <summary>
        /// Return item of type <typeparamref name="TValue"/> with key <paramref name="key"/>. 
        /// If <paramref name="dictionary"/> don't contains item with such <paramref name="key"/>, thrown erro with <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private TValue GetItem<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string message)
        {
            if (!dictionary.ContainsKey(key))
            {
                throw new ArgumentException(message);
            }
            return dictionary[key];
        }

        /// <summary>
        /// Validate if <paramref name="origin"/> point of flight is different from <paramref name="destination"/>.
        /// Return IAirport objects for <paramref name="origin"/> and <paramref name="destination"/>.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <param name="message"></param>
        private Tuple<IAirport, IAirport> ValidateFlightDestination(string origin, string destination)
        {
            if (origin == destination)
            {
                throw new ArgumentException(WRONG_DESTINATION);
            }
            var originAirport = GetItem(_airports, origin, String.Format(MISSING_ITEM, "Airport", "name", origin));
            var destinationAirport = GetItem(_airports, destination, String.Format(MISSING_ITEM, "Airport", "name", destination));
            return new Tuple<IAirport, IAirport>(originAirport, destinationAirport);
        }

        /// <summary>
        /// Validate if this given <paramref name="airline"/> have <paramref name="flight"/>.
        /// Thrown error if condition is not true.
        /// </summary>
        /// <param name="flight"></param>
        /// <param name="airline"></param>
        private IFlight AirlaneGotFlight(string flightId, string airline)
        {
            var flight = GetItem(_flights, flightId, String.Format(MISSING_ITEM, "Flight", "id", flightId));
            if (flight.Airline.Name != airline)
            {
                throw new ArgumentException(String.Format(MISSING_FLIGHT_FROM_AIRLINE, airline, flight.Id));
            }
            ValidateFlightDate(flight.Date, DEPARTED_FLIGHT);
            return flight;
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

        private void ChekIfSeatIsBooked(int row, int column, IFlightSection flightSection)
        {
            var seat = flightSection.Seats[row, column];
            if (seat.Booked)
            {
                throw new ArgumentException(BOOEKD_SEAT);
            }
        }

        private void CheckIfSeatIsValid(int row, int column, IFlightSection flightSection)
        {
            var rows = flightSection.Seats.GetLength(0);
            var columns = flightSection.Seats.GetLength(1);

            if (row < 0 || row >= rows)
            {
                throw new ArgumentException(INVALID_SEAT_ROW);
            }

            if (column < 0 || column >= columns)
            {
                throw new ArgumentException(INVALID_SEAT_COLUMN);
            }
        }

        private T GetEnum<T>(object value)
        {
            var seatClass = (T)value;
            return Enum.IsDefined(typeof(T), value) ? seatClass : throw new ArgumentException("Seat class is not valid");
        }

        private IFlightSection GetFlightSection(IFlight flight, int seatClass)
        {
            var seatClassEnum = GetEnum<SeatClass>(seatClass);
            return GetItem(flight.FlightSections, seatClassEnum, String.Format(MISSING_ITEM, "Section", "name", seatClassEnum.ToString()));
        }

        private void ValidateCountOfSeats(int value, string @type, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(String.Format(INVALID_SEAT_COUNT, type, min, max));
            }
        }

    }
}
