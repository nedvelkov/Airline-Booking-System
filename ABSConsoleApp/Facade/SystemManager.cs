namespace Facade
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Models;
    using Models.Enums;
    using Models.Contracts;
    using System.Globalization;

    public class SystemManager : ISystemManager
    {
        private List<Airline> airlines;
        private List<Airport> airports;
        public SystemManager()
        {
            this.airlines = new List<Airline>();
            this.airports = new List<Airport>();
        }

        public string CreateAirport(string name)
        {
            try
            {
                HasItem(this.airports, x => x.Name == name);
                var airport = new Airport(name);
                this.airports.Add(airport);
            }
            catch (Exception a)
            {
                return a.Message;
            }
            return $"Airport {name} is created successfully";
        }

        public string CreateAirline(string name)
        {
            try
            {
                HasItem(this.airlines, x => x.Name == name);
                var airline = new Airline(name);
                this.airlines.Add(airline);
            }
            catch (Exception a)
            {
                return a.Message;
            }
            return $"Airline {name} is created successfully";

        }

        public string CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            try
            {
                var airline = GetItem(this.airlines, x => x.Name == airlineName, $"Airline with name {airlineName} don't exist");
                var originAirport = GetItem(this.airports, x => x.Name == origin, $"Airport with name {origin} don't exist");
                var destinationAirport = GetItem(this.airports, x => x.Name == destination, $"Airport with name {destination} don't exist");
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
                //var flight = GetItem(GetItem(this.airlines, x => x.Name == airlineName).Flights.ToList(), x => x.Id == flightId,false);
                var airline = GetItem(this.airlines, x => x.Name == airlineName, $"Airline with name {airlineName} don't exist");
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
                originAirport = GetItem(this.airports, x => x.Name == origin, $"Airport with name {origin} don't exist");
                destinationAirport = GetItem(this.airports, x => x.Name == destination, $"Airport with name {destination} don't exist");

            }
            catch (Exception a)
            {

                return a.Message;
            }
            var flights = new List<Flight>();
            foreach (var airline in this.airlines)
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
                var airline = GetItem(this.airlines, x => x.Name == airlineName, $"Airline with name {airlineName} don't exist");
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
            sb.AppendLine($"Airort aviable {this.airports.Count}");
            if (this.airports.Count > 0)
            {
                this.airports.ForEach(x => sb.AppendLine(x.ToString()));
            }
            sb.AppendLine($"Airline aviable {this.airlines.Count}");
            if (this.airlines.Count > 0)
            {
                this.airlines.ForEach(x => sb.AppendLine(x.ToString()));
            }
            return sb.ToString().Trim();
        }

        private T GetItem<T>(List<T> list, Func<T, bool> func, string exceptionMessage)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem == null)
            {
                throw new ArgumentException(exceptionMessage);
            }
            return getItem;
        }

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

        private SeatClass GetSeatClass(int value)
        {
            var seatClass = (SeatClass)value;
            return Enum.IsDefined(typeof(SeatClass), value) ? seatClass : throw new InvalidCastException("Seat class is not valid");
        }
    }
}
