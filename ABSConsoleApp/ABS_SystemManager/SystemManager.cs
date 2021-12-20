using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ABS_SystemManager.Models;
using ABS_SystemManager.Interfaces;
using ABS_DataConstants;
using static ABS_SystemManager.DataConstants.Success;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;
using static ABS_SystemManager.DataConstants.SystemError;
using static ABS_DataConstants.Error;

namespace ABS_SystemManager
{
    public class SystemManager : ISystemManager
    {
        private Dictionary<string, IAirline> _airlines;
        private Dictionary<string, IAirport> _airports;
        private Dictionary<string, IFlight> _flights;

        private DateTime _testDate;

        public SystemManager()
        {
            _airlines = new Dictionary<string, IAirline>();
            _airports = new Dictionary<string, IAirport>();
            _flights = new Dictionary<string, IFlight>();
        }

        public string CreateAirport(string name)
        {
            try
            {
                ValidateString(name, DataConstrain.EVALUATE_AIRPORT_NAME, Error.AIRPORT_TOOLTIP);
                ContainsItem(_airports, name, String.Format(Error.DUBLICATE_ITEM, "Airport", "name"));
            }
            catch (Exception a)
            {

                return a.Message;
            }

            _airports.Add(name, new Airport() { Name = name });

            return String.Format(SUCCESSFUL_CREATED_AIRPORT, name);
        }

        public string CreateAirline(string name)
        {
            try
            {
                ValidateString(name, DataConstrain.EVALUATE_AIRLINE_NAME, Error.AIRLINE_TOOLTIP);
                ContainsItem(_airlines, name, String.Format(Error.DUBLICATE_ITEM, "Airline", "name"));
            }
            catch (Exception a)
            {

                return a.Message;
            }

            _airlines.Add(name, new Airline() { Name = name });

            return String.Format(SUCCESSFUL_CREATED_AIRLINE, name);
        }

        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            IAirline airline;
            IAirport originAirport;
            IAirport destinationAirport;
            DateTime date;

            try
            {
                airline = GetItem(_airlines, airlineName, String.Format(Error.MISSING_ITEM, "Airline", "name", airlineName));
                (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);
                date = ValidateDate(year, month, day);

                ValidateFlightDate(date, INVALID_DATE_OF_DEPARTURE_FLIGHT);
                ValidateString(id, DataConstrain.EVALUATE_FLIGHT_ID, Error.FLIGHT_TOOLTIP);
                ContainsItem(_flights, id, String.Format(Error.DUBLICATE_ITEM, "Flight", "id"));
            }
            catch (Exception a)
            {
                return a.Message;
            }

            var flight = GetFlight(airline, originAirport, destinationAirport, date, id);

            _flights.Add(id, flight);

            return String.Format(SUCCESSFUL_CREATED_FLIGHT, origin, destination, airlineName);
        }

        public string CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass)
        {
            IFlight flight;
            IFlightSection flightSection;

            try
            {
                flight = AirlaneGotFlight(flightId, airlineName);

                flightSection = CreateFlightSection(rows, columns, seatClass);

                ContainsItem(flight.FlightSections, (SeatClass)seatClass, String.Format(Error.DUBLICATE_ITEM, "Flight section", "name"));
            }
            catch (Exception a)
            {
                return a.Message;
            }

            flight.AddFlightSection(flightSection);

            return String.Format(SUCCESSFUL_CREATED_FLIGHT_SECTION, (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);
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

        private DateTime ValidateDate(int year, int month, int day)
        {
            DateTime date;
            var validDate = DateTime.TryParse($"{month}/{day}/{year}", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (!validDate)
            {
                throw new ArgumentException(INVALID_DATE);
            }
            return date;
        }

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
        /// Determines whether the <paramref name="dictionary"/> contains item <typeparamref name="TValue"/> the specified <paramref name="key"/>.
        /// Throw error with <paramref name="message"/> if is true.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        private void ContainsItem<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, string message)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="dictionary"/> contains item <typeparamref name="TValue"/> the specified <paramref name="key"/>.
        /// Throw error with <paramref name="message"/> if is true.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        private void ContainsItem<TKey, TValue>(IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, string message)
        {
            if (dictionary.ContainsKey(key))
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
            var originAirport = GetItem(_airports, origin, String.Format(Error.MISSING_ITEM, "Airport", "name", origin));
            var destinationAirport = GetItem(_airports, destination, String.Format(Error.MISSING_ITEM, "Airport", "name", destination));
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
            var flight = GetItem(_flights, flightId, String.Format(Error.MISSING_ITEM, "Flight", "id", flightId));
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

        private IFlightSection CreateFlightSection(int rows, int colms, int seatClass)
        {

            var seatClassEnum = GetSeatClass(seatClass);
            var flightSection = new FlightSection() { SeatClass = seatClassEnum };

            ValidateCountOfSeats(rows, "Rows", DataConstrain.MIN_SEAT_ROWS, DataConstrain.MAX_SEAT_ROWS);
            ValidateCountOfSeats(colms, "Columns", DataConstrain.MIN_SEAT_COLUMNS, DataConstrain.MAX_SEAT_COLUMNS);
            var seats = SeatCollection(rows, colms);

            flightSection.AddSeats(seats);

            return flightSection;
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
                throw new ArgumentException(Error.INVALID_SEAT_ROW);
            }

            if (column < 0 || column >= columns)
            {
                throw new ArgumentException(Error.INVALID_SEAT_COLUMN);
            }
        }

        private SeatClass GetSeatClass(int value)
        {
            var seatClass = (SeatClass)value;
            return Enum.IsDefined(typeof(SeatClass), value) ? seatClass : throw new InvalidCastException("Seat class is not valid");
        }

        private IFlightSection GetFlightSection(IFlight flight, int seatClass)
        {
            var seatClassEnum = GetSeatClass(seatClass);
            return GetItem(flight.FlightSections, seatClassEnum, String.Format(Error.MISSING_ITEM, "Section", "name", seatClassEnum.ToString()));
        }

        private void ValidateCountOfSeats(int value, string @type, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(String.Format(INVALID_SEAT_COUNT, type, min, max));
            }
        }

        private ISeat[,] SeatCollection(int rows, int columns)
        {
            var seats = new ISeat[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var columnCharAsInt = column + INITIAL_VALUE_FOR_SEAT_COLUMN_CHAR;
                    var seat = new Seat()
                    {
                        Row = row + 1,
                        Column = (char)columnCharAsInt
                    };
                    seats[row, column] = seat;
                }
            }

            return seats;
        }

        private void ValidateSeatNumber(int row, char column)
        {
            if (row < DataConstrain.MIN_SEAT_ROWS || row > DataConstrain.MAX_SEAT_ROWS)
            {
                throw new ArgumentException(Error.INVALID_SEAT_ROW);
            }
            if (column < FIRST_SEAT_COLUMN_AS_CHAR || column > LAST_SEAT_COLUMN_AS_CHAR)
            {
                throw new ArgumentException(Error.INVALID_SEAT_COLUMN);
            }
        }

        private IFlight GetFlight(IAirline airline, IAirport originAirport, IAirport destinationAirport, DateTime date, string id)
        {
            var flight = new Flight()
            {
                Airline = airline,
                Origin = originAirport,
                Destination = destinationAirport,
                Date = date,
                Id = id,
            };

            airline.AddFlight(flight);
            originAirport.AddDeparureFlight(flight.Id);
            destinationAirport.AddArrivalFlight(flight.Id);

            return flight;
        }
    }
}
