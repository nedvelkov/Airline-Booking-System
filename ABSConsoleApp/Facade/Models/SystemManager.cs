namespace Facade.Models
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Interfaces;
    using System.Globalization;
    using System.Text.RegularExpressions;

    using DataConstants;

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

                _airports.Add(name, new Airport() { Name = name });

                return String.Format(Success.createdAirport, name);
            }
            catch (Exception a)
            {

                return a.Message;
            }
        }

        public string CreateAirline(string name)
        {
            try
            {
                ValidateString(name, DataConstrain.evaluateAirlineName, Error.airlineName);
                ContainsItem(_airlines, name, String.Format(Error.dublicateItem, "Airline", "name"));

                _airlines.Add(name, new Airline() { Name = name });

                return String.Format(Success.createdAirline, name);
            }
            catch (Exception a)
            {

                return a.Message;
            }
        }

        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            try
            {
                var airline = GetItem(_airlines, airlineName, String.Format(Error.missingItem, "Airline", "name", airlineName));
                var (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);
                var date = ValidateDate(year, month, day);

                ValidateFlightDate(date, Error.notValidFlightDate);
                ValidateString(id, DataConstrain.evaluateFlightId, Error.flightId);
                ContainsItem(_flights, id, String.Format(Error.dublicateItem, "Flight", "id"));

                var flight = GetFlight(airline, originAirport, destinationAirport, date, id);

                _flights.Add(id, flight);

                return String.Format(Success.createFlight, origin, destination, airlineName);

            }
            catch (Exception a)
            {
                return a.Message;
            }
        }

        public string CreateSection(string airlineName, string flightId, int rows, int colms, int seatClass)
        {
            try
            {
                var flight = AirlaneGotFlight(flightId, airlineName);

                var flightSection = CreateFlightSection(rows, colms, seatClass);

                ContainsItem(flight.FlightSections, (SeatClass)seatClass, String.Format(Error.dublicateItem, "Flight section", "name"));

                flight.AddFlightSection(flightSection);

                return String.Format(Success.createFlightSection, (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);
            }
            catch (Exception a)
            {
                return a.Message;
            }
        }

        /// <summary>
        /// This method is only for test purpose.
        /// </summary>
        /// <param name="airlineName"></param>
        /// <param name="flightId"></param>
        /// <param name="rows"></param>
        /// <param name="colms"></param>
        /// <param name="seatClass"></param>
        /// <param name="sectionDate"></param>
        /// <returns></returns>
        public string CreateSection(string airlineName, string flightId, int rows, int colms, int seatClass,DateTime sectionDate)
        {
            _testDate = sectionDate;
            try
            {
                var flight = AirlaneGotFlight(flightId, airlineName);

                var flightSection = CreateFlightSection(rows, colms, seatClass);

                ContainsItem(flight.FlightSections, (SeatClass)seatClass, String.Format(Error.dublicateItem, "Flight section", "name"));

                flight.AddFlightSection(flightSection);

                return String.Format(Success.createFlightSection, (SeatClass)seatClass, flight.Origin.Name, flight.Destination.Name, airlineName);
            }
            catch (Exception a)
            {
                return a.Message;
            }
        }

        public string FindAvailableFlights(string origin, string destination)
        {
            try
            {
                var (originAirport, destinationAirport) = ValidateFlightDestination(origin, destination);

                var sb = new StringBuilder();

                var flights = originAirport.DeparturesFlights()
                             .Where(x => x.Value.Destination.Equals(destinationAirport)  // Filter collection and take flights with give destination
                             && DateTime.Compare(x.Value.Date,DateTime.UtcNow)>=0)       // Validate flight date
                             .Select(x => x.Value)                                       // Select IFlight objects
                             .ToList();                                                  // Convert to List<IFlight> objects
                flights.ForEach(f => sb.AppendLine(f.ToString()));                       // Append every IFlight object to strig and add it to StringBuilder sb

                return flights.Count > 0 ? sb.ToString() : String.Format(Error.noFlights, origin, destination);

            }
            catch (Exception a)
            {

                return a.Message;
            }
        }

        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char colmn)
        {
            try
            {
                var flight = AirlaneGotFlight(flightId, airlineName);

                var flightSection = GetFlightSection(flight, seatClass);

                var seatNumber = GetSeatNumber(row, colmn);

                ChekIfSeatIsBooked(seatNumber, flightSection);

                flightSection.BookSeat(seatNumber);

                return String.Format(Success.bookedSeat,seatNumber,(SeatClass)seatClass,flight.Origin.Name,flight.Destination.Name,airlineName);

            }
            catch (Exception a)
            {
                return a.Message;
            }

        }

        public string DisplaySystemDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Airort aviable {_airports.Count}");
            if (_airports.Count > 0)
            {
                _airports.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }
            sb.AppendLine($"Airline aviable {_airlines.Count}");
            if (_airlines.Count > 0)
            {
                _airlines.Select(x => x.Value).ToList().ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }

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
            ValidateFlightDate(flight.Date,Error.departedFlight);
            return flight;
        }

        /// <summary>
        /// Check if <paramref name="date"/> is in past.Throw error with <paramref name="message"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        private void ValidateFlightDate(DateTime date,string message)
        {
            var dateToCompare = _testDate != DateTime.MinValue ? _testDate : DateTime.UtcNow;
            if (DateTime.Compare(date.Date, dateToCompare.Date) < 0)
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

        private ISeatNumber GetSeatNumber(int row, char colmn)
        {
            ValidateSeatNumber(row, colmn);
            return new SeatNumber()
            {
                Row = row,
                Colmn = colmn
            };
        }

        private void ChekIfSeatIsBooked(ISeatNumber number,IFlightSection flightSection)
        {
            var seat = GetItem(flightSection.Seats, number, String.Format(Error.missingItem, "Seat", "number", number.ToString()));
            if (seat.Booked)
            {
                throw new ArgumentException(Error.bookedSeat);
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

        private IEnumerable<ISeat> SeatCollection(int rows, int colms)
        {
            var seats = new List<ISeat>();
            for (int row = 1; row <= rows; row++)
            {
                for (int colmn = 1; colmn <= colms; colmn++)
                {
                    var seatNumber = new SeatNumber()
                    {
                        Row = row,
                        Colmn = (char)(colmn + DataConstrain.initialValueForSeatColm)
                    };

                    var seat = new Seat() { Number = seatNumber };
                    seats.Add(seat);
                }
            }

            return seats;
        }

        private void ValidateSeatNumber(int row, char colmn)
        {
            if (row < DataConstrain.minSeatRows && row > DataConstrain.maxSeatRows)
            {
                throw new ArgumentException(Error.invalidSeatRow);
            }
            if (colmn < DataConstrain.firstSeatChar && colmn > DataConstrain.lastSeatChar)
            {
                throw new ArgumentException(Error.invalidSeatColmn);
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
            originAirport.AddDeparureFlight(flight);

            return flight;
        }
    }
}
