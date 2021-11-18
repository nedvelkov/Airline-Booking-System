namespace Facade
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Models;
    using Models.Enums;
    using System.Text;

    public class SystemManager:ISystemManager
    {
        private List<Airline> airlines;
        private List<Airport> airports;
        public SystemManager()
        {
            this.airlines = new List<Airline>();
            this.airports = new List<Airport>();
        }

        public void CreateAirport(string name)
        {
            HasItem(this.airports, x => x.Name == name);
            var airport = new Airport(name);
            this.airports.Add(airport);
        }

        public void CreateAirline(string name)
        {
            HasItem(this.airlines, x => x.Name == name);
            var airline = new Airline(name);
            this.airlines.Add(airline);

        }

        public void CreateFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //TODO: validate Date -> throw error if date is not valid
            var airline = GetItem(this.airlines, x => x.Name == airlineName);
            var originAirport = GetItem(this.airports, x => x.Name == origin);
            var destinationAirport = GetItem(this.airports, x => x.Name == destination);
            var date = new DateTime(year, month, day);
            var flight = new Flight(originAirport, destinationAirport, date, id);

            airline.AddFlight(flight);
        }

        public void CreateSection(string airlineName, string flightId, int rows, int colms, SeatClass seatClass)
        {
            //var flight = GetItem(GetItem(this.airlines, x => x.Name == airlineName).Flights.ToList(), x => x.Id == flightId,false);
            var airline = GetItem(this.airlines, x => x.Name == airlineName);
            var flight = GetItem(airline.Flights.ToList(), x => x.Id == flightId,false);

            flight.AddFlightSection(seatClass, rows, colms);
        }

        public string FindAvailableFlights(string origin, string destination)
        {
            var originAirport = GetItem(this.airports, x => x.Name == origin);
            var destinationAirport = GetItem(this.airports, x => x.Name == destination);
            var flights = new List<Flight>();
            foreach (var airline in this.airlines)
            {
                var tmp = airline.Flights.ToList().Where(x => x.Origin.Equals(originAirport) && x.Destination.Equals(destinationAirport));
                flights.AddRange(tmp);
            }

            var sb = new StringBuilder();
            flights.ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().Trim();

        }

        public void BookSeat(string airlineName, string flightId, SeatClass seatClass, int row, char colmn)
        {
            var airline = GetItem(this.airlines, x => x.Name == airlineName);
            var flight = GetItem(airline.Flights.ToList(), x => x.Id == flightId, false);
            var flightSections= GetItem(flight.FlightSections.ToList(),x=>x.SeatClass==seatClass);

            flightSections.BookSeat(row, colmn);
        }

        public void DisplaySystemDetails()
        {
            throw new NotImplementedException();
        }

        private T GetItem<T>(List<T> list, Func<T, bool> func,bool flag=true)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem == null)
            {
                var name = flag ? "name" : "id";
                throw new ArgumentNullException($"{getItem.GetType().Name} with this ${name} don't exist.");
            }
            return getItem;
        }

        private bool HasItem<T>(List<T> list, Func<T, bool> func)
        {
            var getItem = list.FirstOrDefault(func);
            if (getItem != null)
            {
                throw new ArgumentNullException($"{getItem.GetType().Name} with this name already exist.");
            }
            return false;
        }

    }
}
