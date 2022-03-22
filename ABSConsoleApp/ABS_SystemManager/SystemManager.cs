using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


using ABS_SystemManager.Interfaces;
using ABS_SystemManager.Data.UserDefineModels;
using ABS_Models;

using static ABS_DataConstants.DataConstrain;
using static ABS_SystemManager.DataConstants.Success;
using static ABS_SystemManager.DataConstants.SystemDataConstrain;
using static ABS_SystemManager.DataConstants.SystemError;
using static ABS_DataConstants.Error;


namespace ABS_SystemManager
{
    public class SystemManager : ISystemManager
    {
        private readonly IAbsRepository _repository;
        private DateTime _testDate;

        public SystemManager(IAbsRepository repository) => _repository = repository;

        public async Task<string> CreateAirport(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);

                await _repository.CreateAirport(name);
            }
            catch (Exception a)
            {

                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_AIRPORT, name);
        }

        public async Task<bool> HasAirport(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRPORT_NAME, AIRPORT_TOOLTIP);

                return await _repository.HasAirport(name);
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<string> CreateAirline(string name)
        {
            try
            {
                ValidateString(name, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);
                await _repository.CreateAirline(name);
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

                await _repository.CreateFlight(airlineName, origin, destination, year, month, day, id);
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

                await _repository.CreateSection(airlineName, flightId, rows, columns, seatClass);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return string.Format(SUCCESSFUL_CREATED_FLIGHT_SECTION, (SeatClass)seatClass, flightId, airlineName);
        }

        public async Task<List<FlightsModel>> FindAvailableFlights(string origin, string destination)
        {
            List<FlightsModel> flights;
            try
            {
                ValidateFlightDestination(origin, destination);
                flights = await _repository.FindAvailableFlights(origin, destination);
            }
            catch (Exception a)
            {
                return null;
            }
                                 
            return flights;

        }

        public async Task<string> BookSeat(string airlineName, string flightId, int seatClass, int row, char column)
        {
            try
            {
                ValidateString(flightId, EVALUATE_FLIGHT_ID, FLIGHT_TOOLTIP);
                ValidateString(airlineName, EVALUATE_AIRLINE_NAME, AIRLINE_TOOLTIP);

                var lastSeatNumber = await _repository.GetLastSeatNumber(flightId, seatClass);
                CheckIfSeatIsValid(row, column, lastSeatNumber);

                await _repository.BookSeat(airlineName, flightId, seatClass, row, column);
            }
            catch (Exception a)
            {
                return a.Message;
            }

            return string.Format(SUCCESSFUL_BOOKED_SEAT, string.Format(SEAT_NUMBER_TO_STRING, row, column), (SeatClass)seatClass, flightId, airlineName);

        }

        public async Task<string> DisplaySystemDetails()
        {
            var sb = new StringBuilder();
            sb.AppendLine(DISPLAY_AIRPORTS_TITLE);

            _repository.ListAirports
                        .Select(x => string.Format(AIRPORT_TO_STRING_TITLE, x))
                        .ToList()
                        .ForEach(x => sb.AppendLine(x.ToString()));

            sb.AppendLine(DISPLAY_AIRLINES_TITLE);

            var airlines = await _repository.GetAirlineViews();
            if (airlines.Count > 0)
            {
                airlines.ForEach(x => sb.AppendLine(x.ToString()));
            }

            return sb.ToString().Trim();
        }
        //TODO :Clean unused methos
        public async Task<SystemDetailsModel> DisplaySystemDetailsAsModel()
        {

            var airports = _repository.ListAirports
                         .ToList();

            var airlines = await _repository.GetAirlineWithFlightsViews();

            return new SystemDetailsModel { AirportList = airports, AirlineList = airlines };
        }

        public IReadOnlyList<string> ListAirlines => _repository.ListAirlines;

        public IReadOnlyList<string> ListAirports => _repository.ListAirports;

        public IReadOnlyList<string> ListFlights => _repository.ListFlights;

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

            if (column < FIRST_SEAT_COLUMN_AS_CHAR || column > seatNumber.Column[0])
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

        public Task<List<FlightsModel>> GetFlightsByAirlineName(string airlineName) => _repository.GetFlightsByAirlineName(airlineName);
        public async Task<List<FlightSectionView>> GetFlightSectionsForFlight(string flightId)
        {
            var list = await _repository.GetFlightSectionsForFlight(flightId);
           return list.Select(x =>
             {
               return  new FlightSectionView() { FlightSectionId = x.FlightSectionId, Seats = x.Seats, SeatClass = GetEnumAsString(x.SeatClass) };
             }).ToList();
        }

        private string GetEnumAsString (int value)
        {
            var enumValue = (SeatClass)value;
            return enumValue.ToString();
        }
    }
}
