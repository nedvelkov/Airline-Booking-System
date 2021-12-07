namespace ABS_SystemManager
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using ABS_SystemManager.Models;
    using Interfaces;

    using DataConstants;
    using System.Threading.Tasks;

    public class SystemManager : ISystemManager
    {
        private Dictionary<string, IAirline> _airlines;
        private Dictionary<string, IAirport> _airports;
        private Dictionary<string, IFlight> _flights;

        private DateTime _testDate;

        public SystemManager()
        {
            _airlines = new();
            _airports = new();
            _flights = new();
        }

        public string CreateAirport(string name)
        {
            try
            {
                ValidateString(name, DataConstrain.evaluateAirportName, Error.airportName);
                ContainsItem(_airports, name, String.Format(Error.dublicateItem, "Airport", "name"));
            }
            catch (Exception a)
            {

                return a.Message;
            }

            _airports.Add(name, new Airport() { Name = name });

            return String.Format(Success.createdAirport, name);
        }

        public string CreateAirline(string name)
        {
            try
            {
                ValidateString(name, DataConstrain.evaluateAirlineName, Error.airlineName);
                ContainsItem(_airlines, name, String.Format(Error.dublicateItem, "Airline", "name"));
            }
            catch (Exception a)
            {

                return a.Message;
            }

            _airlines.Add(name, new Airline() { Name = name });

            return String.Format(Success.createdAirline, name);
        }

        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            IAirline airline;
            IAirport originAirport;
            IAirport destinationAirport;
            DateTime date;

            try
            {
                airline = GetItem(_airlines, airlineName, String.Format(Error.missingItem, "Airline", "name", airlineName));
                (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);
                date = ValidateDate(year, month, day);

                ValidateFlightDate(date, Error.notValidFlightDate);
                ValidateString(id, DataConstrain.evaluateFlightId, Error.flightId);
                ContainsItem(_flights, id, String.Format(Error.dublicateItem, "Flight", "id"));
            }
            catch (Exception a)
            {
                return a.Message;
            }

            var flight = GetFlight(airline, originAirport, destinationAirport, date, id);

            _flights.Add(id, flight);

            return String.Format(Success.createFlight, origin, destination, airlineName);
        }

        public async Task<string> CreateFlightAsync(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            IAirline airline;
            IAirport originAirport;
            IAirport destinationAirport;
            DateTime date;

            try
            {
                airline = await Task.Run(() => GetItem(_airlines, airlineName, String.Format(Error.missingItem, "Airline", "name", airlineName)));
                (originAirport, destinationAirport) = await Task.Run(() => ValidateFlightDestination(origin, destination));
                date = await Task.Run(() => ValidateDate(year, month, day));

                ValidateFlightDate(date, Error.notValidFlightDate);
                ValidateString(id, DataConstrain.evaluateFlightId, Error.flightId);
                ContainsItem(_flights, id, String.Format(Error.dublicateItem, "Flight", "id"));
            }
            catch (Exception a)
            {
                return a.Message;
            }

            var flight = await Task.Run(() => GetFlight(airline, originAirport, destinationAirport, date, id));

            _flights.Add(id, flight);

            return String.Format(Success.createFlight, origin, destination, airlineName);
        }

        public string CreateSection(string airlineName, string flightId, int rows, int columns, int seatClass)
        {
            IFlight flight;
            IFlightSection flightSection;

            try
            {
                flight = AirlaneGotFlight(flightId, airlineName);

                flightSection = CreateFlightSection(rows, columns, seatClass);

                ContainsItem(flight.FlightSections, (SeatClass)seatClass, String.Format(Error.dublicateItem, "Flight section", "name"));
            }
            catch (Exception a)
            {
                return a.Message;
            }

            flight.AddFlightSection(flightSection);

            return String.Format(Success.createFlightSection, (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);
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
                                            && DateTime.Compare(x.Value.Date, DateTime.UtcNow.Date) > 0)
                                            .Select(x => x.Value)
                                            .ToList();
            flights.ForEach(f => sb.AppendLine(f.ToString()));

            return flights.Count > 0 ? sb.ToString() : String.Format(Error.noFlights, origin, destination);

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

            return flights.Count > 0 ? sb.ToString() : String.Format(Error.noFlights, origin, destination);
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
                arrayColumn = (int)column - DataConstrain.valueForSeatColumn;

                CheckIfSeatIsValid(arrayRow, arrayColumn, flightSection);

                ChekIfSeatIsBooked(arrayRow, arrayColumn, flightSection);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            flightSection.BookSeat(arrayRow, arrayColumn);

            return String.Format(Success.bookedSeat, String.Format(DataConstrain.seatNumber, row, column), (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);

        }

        public string DisplaySystemDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(DataConstrain.displayAirportsTitle, _airports.Count));
            if (_airports.Count > 0)
            {
                _airports.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }
            sb.AppendLine(String.Format(DataConstrain.displayAirlinesTitle, _airlines.Count));
            if (_airlines.Count > 0)
            {
                _airlines.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }

        public IReadOnlyList<string> ListAirlines => _airlines.Select(x => x.Key).ToList();
        public IReadOnlyList<string> ListAirports => _airports.Select(x => x.Key).ToList();
        public IReadOnlyList<string> ListFlights => _flights.Select(x => x.Key).ToList();

        private DateTime ValidateDate(int year, int month, int day)
        {
            DateTime date;
            var validDate = DateTime.TryParse($"{month}/{day}/{year}", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (!validDate)
            {
                throw new ArgumentException(Error.notValidDate);
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
                throw new ArgumentException(Error.wrongDestination);
            }
            var originAirport = GetItem(_airports, origin, String.Format(Error.missingItem, "Airport", "name", origin));
            var destinationAirport = GetItem(_airports, destination, String.Format(Error.missingItem, "Airport", "name", destination));
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
            var flight = GetItem(_flights, flightId, String.Format(Error.missingItem, "Flight", "id", flightId));
            if (flight.Airline.Name != airline)
            {
                throw new ArgumentException(String.Format(Error.invalidFlightId, airline, flight.Id));
            }
            ValidateFlightDate(flight.Date, Error.departedFlight);
            return flight;
        }

        /// <summary>
        /// Check if <paramref name="date"/> is in past.Throw error with <paramref name="message"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        private void ValidateFlightDate(DateTime date, string message)
        {
            var dateToCompare = _testDate != DateTime.MinValue ? _testDate : DateTime.UtcNow.Date;
            if (DateTime.Compare(date.Date, dateToCompare) <= 0)
            {
                throw new ArgumentException(message);
            }
        }

        private IFlightSection CreateFlightSection(int rows, int colms, int seatClass)
        {

            var seatClassEnum = GetSeatClass(seatClass);
            var flightSection = new FlightSection() { SeatClass = seatClassEnum };

            ValidateCountOfSeats(rows, "Rows", DataConstrain.minSeatRows, DataConstrain.maxSeatRows);
            ValidateCountOfSeats(colms, "Columns", DataConstrain.minSeatColms, DataConstrain.maxSeatColms);
            var seats = SeatCollection(rows, colms);

            flightSection.AddSeats(seats);

            return flightSection;
        }

        private void ChekIfSeatIsBooked(int row, int column, IFlightSection flightSection)
        {
            var seat = flightSection.Seats[row, column];
            if (seat.Booked)
            {
                throw new ArgumentException(Error.bookedSeat);
            }
        }

        private void CheckIfSeatIsValid(int row, int column, IFlightSection flightSection)
        {
            var rows = flightSection.Seats.GetLength(0);
            var columns = flightSection.Seats.GetLength(1);

            if (row < 0 || row >= rows)
            {
                throw new ArgumentException(Error.invalidSeatRow);
            }

            if (column < 0 || column >= columns)
            {
                throw new ArgumentException(Error.invalidSeatColumn);
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
            return GetItem(flight.FlightSections, seatClassEnum, String.Format(Error.missingItem, "Section", "name", seatClassEnum.ToString()));
        }

        private void ValidateCountOfSeats(int value, string @type, int min, int max)
        {
            if (value < min || value > max)
            {
                throw new ArgumentException(String.Format(Error.invalidCount, type, min, max));
            }
        }

        private ISeat[,] SeatCollection(int rows, int columns)
        {
            var seats = new ISeat[rows, columns];
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var columnCharAsInt = column + DataConstrain.initialValueForSeatColumn;
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
            if (row < DataConstrain.minSeatRows || row > DataConstrain.maxSeatRows)
            {
                throw new ArgumentException(Error.invalidSeatRow);
            }
            if (column < DataConstrain.firstSeatChar || column > DataConstrain.lastSeatChar)
            {
                throw new ArgumentException(Error.invalidSeatColumn);
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
