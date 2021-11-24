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
        private Dictionary<string, Airline> _airlines;
        private Dictionary<string, Airport> _airports;
        private Dictionary<string, Flight> _flights;

        public SystemManager()
        {
            _airlines = new();
            _airports = new();
        }

        public string CreateAirport(string name)
        {
            try
            {
                ValidateName(name, "^[A-Z]{3}$", Error.airportName);
                ContainsItem(_airports, name, String.Format(Error.dublicateName, "Airport", name));

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
                ValidateName(name, "^[a-zA-Z]{1,5}$", Error.airlineName);
                ContainsItem(_airlines, name, String.Format(Error.dublicateName, "Airline", name));

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
                var airline = GetItem(_airlines, airlineName, String.Format(Error.airlineMissing, airlineName));
                var originAirport = GetItem(_airports, origin, String.Format(Error.airportMissing, origin));
                var destinationAirport = GetItem(_airports, destination, String.Format(Error.airportMissing, destination));
                var date = ValidateDate(year, month, day);

                var flight = new Flight(originAirport, destinationAirport, date, id);

                airline.AddFlight(flight);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return $"Flight from {origin} to {destination} on airline {airlineName} is created successfully";

        }

        public string CreateSection(string airlineName, string flightId, int rows, int colms, int seatClass)
        {
            try
            {
                //var flight = GetItem(GetItem(airlines, x => x.Name == airlineName).Flights.ToList(), x => x.Id == flightId,false);
                var airline = GetItem(_airlines, x => x.Name == airlineName, $"Airline with name {airlineName} don't exist");
                var flight = GetItem(airline.Flights.ToList(), x => x.Id == flightId, $"Flight with id {flightId} don't exist");

                flight.AddFlightSection((SeatClass)seatClass, rows, colms);

                return $"Section {(SeatClass)seatClass} class is created successfully for flight from {flight.Origin.Name} to {flight.Destination.Name} on airline {airlineName}";
            }
            catch (Exception a)
            {
                return a.Message;
            }
        }

        public string FindAvailableFlights(string origin, string destination)
        {
            if (destination.Equals(origin))
            {
                return "Origin point can't be same as destination";
            }
            IAirport originAirport;
            IAirport destinationAirport;
            try
            {
                originAirport = GetItem(_airports, x => x.Name == origin, $"Airport with name {origin} don't exist");
                destinationAirport = GetItem(_airports, x => x.Name == destination, $"Airport with name {destination} don't exist");

            }
            catch (Exception a)
            {

                return a.Message;
            }
            var flights = new List<Flight>();
            foreach (var airline in _airlines)
            {
                var tmp = airline.Flights.ToList().Where(x => x.Origin.Equals(originAirport) && x.Destination.Equals(destinationAirport));
                flights.AddRange(tmp.Select(x => (Flight)x));
            }

            if (flights.Count > 0)
            {
                var sb = new StringBuilder();
                flights.ForEach(x => sb.AppendLine(x.ToString()));

                return sb.ToString().Trim();
            }
            return $"There is no flight from {origin} to {destination}, at this time";
        }

        public string BookSeat(string airlineName, string flightId, int seatClass, int row, char colmn)
        {
            try
            {
                var seatClassEnum = GetSeatClass(seatClass);
                var airline = GetItem(_airlines, x => x.Name == airlineName, $"Airline with name {airlineName} don't exist");
                var flight = GetItem(airline.Flights.ToList(), x => x.Id == flightId, $"Flight with id {flightId} don't exist");
                var flightSections = GetItem(flight.FlightSections.ToList(), x => x.SeatClass == seatClassEnum, $"Section with name {seatClass} don't exist");

                flightSections.BookSeat(row, colmn);

                return $"Seat {row:D3}{colmn} in {seatClass} class is booked for flight from {flight.Origin.Name} to {flight.Destination.Name} on airline {airlineName}";

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
                _airports.ForEach(x => sb.AppendLine(x.ToString()));
            }
            sb.AppendLine($"Airline aviable {_airlines.Count}");
            if (_airlines.Count > 0)
            {
                _airlines.ForEach(x => sb.AppendLine(x.ToString()));
            }
            return sb.ToString().Trim();
        }
        /*
        private T GetItem<T>(List<T> list, Func<T, bool> func, string exceptionMessage)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem == null)
            {
                throw new ArgumentException(exceptionMessage);
            }
            return getItem;
        }
        */
        private bool HasItem<T>(List<T> list, Func<T, bool> func)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem != null)
            {
                throw new ArgumentNullException($"{getItem.GetType().Name} with this name already exist");
            }
            return false;
        }

        private DateTime ValidateDate(int year, int month, int day)
        {
            DateTime date;
            var validDate = DateTime.TryParse($"{month}/{day}/{year}", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
            if (validDate == false)
            {
                throw new ArgumentException("Date is not valid");
            }
            return date;
        }

        /// <summary>
        /// Validate <paramref name="name"/> based on regex <paramref name="expression"/>.Throw error with <paramref name="message"/> if <paramref name="name"/> is not valid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private void ValidateName(string name, string expression, string message)
        {
            var regex = new Regex(expression);
            if (!regex.IsMatch(name))
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Determines whether the <paramref name="dictionary"/> contains item <typeparamref name="T"/> the specified <paramref name="key"/>.Throw error with <paramref name="message"/> if is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        private void ContainsItem<T>(Dictionary<string, T> dictionary, string key, string message)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Return item of type <typeparamref name="T"/> with key <paramref name="key"/>. If <paramref name="dictionary"/> don't contains item with such <paramref name="key"/>, thrown erro with <paramref name="message"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private T GetItem<T>(Dictionary<string, T> dictionary, string key, string message)
        {
            ContainsItem(dictionary, key, message);
            return dictionary[key];
        }

        private SeatClass GetSeatClass(int value)
        {
            var seatClass = (SeatClass)value;
            return Enum.IsDefined(typeof(SeatClass), value) ? seatClass : throw new InvalidCastException("Seat class is not valid");
        }
    }
}
