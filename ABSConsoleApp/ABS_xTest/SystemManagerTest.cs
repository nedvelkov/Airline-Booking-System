namespace ABS_xTest
{

    using System;
    using Xunit;

    using Facade;
    using Facade.Models;
    using Facade.DataConstants;

    public class SystemManagerTest
    {

        [Fact]
        public void CreateValidAirport()
        {
            //Arrange
            var system = new SystemManager();
            var name = "PLD";
            var expected = String.Format(Success.createdAirport, name);

            //Act
            var result = system.CreateAirport(name);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("SA")]
        [InlineData("111")]
        [InlineData("ААА")]
        [InlineData("PLOD")]
        [InlineData("J R")]
        [InlineData("*-*")]
        [InlineData("")]
        [InlineData("           ")]
        [InlineData(null)]
        public void CreateInvalidAirport(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = Error.airportName;

            //Act
            var result = system.CreateAirport(name);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateValidAirline()
        {
            //Arrange
            var system = new SystemManager();
            var name = "BGAir";
            var expected = String.Format(Success.createdAirline, name);

            //Act
            var result = system.CreateAirline(name);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("RayanAir")]
        [InlineData("КАТ")]
        [InlineData("")]
        [InlineData("jM&R")]
        [InlineData("jMore1")]
        [InlineData("   ")]
        public void CreateInvalidAirline(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = Error.airlineName;

            //Act
            var result = system.CreateAirline(name);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void CreateValidFlight(int days)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "VNA";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(days);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreateInvalidFlightWithSameOrigin()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);

            var expected = Error.wrongDestination;

            //Act

            var result = system.CreateFlight(airlineName, origin, origin, year, month, day, id);
            ;
            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateInvalidFlightWithMissingOrigin()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);

            var expected = String.Format(Error.missingItem, "Airport", "name", origin);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateInvalidFlightWithWrongAirline()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();

            var expected = String.Format(Error.missingItem, "Airline", "name", airlineName);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(2021, 18, 5)]
        public void CreateInvalidFlightWithWrongDate(int year, int month, int day)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = Error.notValidDate;

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(2019, 12, 5)]
        public void CreateInvalidFlightWithWrongFlightDate(int year, int month, int day)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = Error.notValidFlightDate;

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void AddFlightWithSameId()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = String.Format(Error.dublicateItem, "Flight", "id");

            //Act
            system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("ЕСR15")]
        [InlineData("R2-D2")]
        [InlineData("5С")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("R(35)")]
        public void CreateInvalidFlightWithWrongId(string id)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = Error.flightId;

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateValidSection()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var flightId = "BG14844";
            var rows = 2;
            var colms = 3;
            var seatClass = 1;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = String.Format(Success.createFlightSection, (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 4, -1)]
        [InlineData(2, 4, 5)]
        [InlineData(2, 4, 0)]
        public void CreateInvalidSectionWithWrongSeatClass(int rows, int colms, int seatClass)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var flightId = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = Error.invalidSeatClass;


            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 4, 1)]
        [InlineData(101, 4, 1)]
        [InlineData(-8, 4, 1)]
        public void CreateInvalidSectionWithWrongRowsCount(int rows, int colms, int seatClass)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var flightId = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = String.Format(Error.invalidSeatCount, "Rows", DataConstrain.minSeatRows, DataConstrain.maxSeatRows);


            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 0, 1)]
        [InlineData(5, 15, 1)]
        [InlineData(10, -4, 1)]
        public void CreateInvalidSectionWithWrongColumnsCount(int rows, int colms, int seatClass)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var flightId = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = String.Format(Error.invalidSeatCount, "Columns", DataConstrain.minSeatColms, DataConstrain.maxSeatColms);


            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // dayOfSection is date of createing section
        // departureDay is day of flight
        // dayOfSection must be bigger than departureDay
        [InlineData(2, 5)]
        [InlineData(5, 15)]
        [InlineData(10, 11)]
        public void TestCreateSectionOnDeparureFlight(int departureDay, int dayOfSection)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(departureDay);
            var yearDeparture = flightDate.Year;
            var monthDeparture = flightDate.Month;
            var dayDeparture = flightDate.Day;

            var flightId = "BG14844";
            var rows = 2;
            var colms = 2;
            var seatClass = 1;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, yearDeparture, monthDeparture, dayDeparture, flightId);

            var expected = Error.departedFlight;


            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass,today.AddDays(dayOfSection));

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFindAvailableFlightsAtleastOne()
        {
            //Arrange
            var origin = "SFA";
            var destination = "PRS";
            var airlineName = "BGAir";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            var expected = String.Format(Success.testFlightToDestination, id, origin, destination, flightDate.ToString(DataConstrain.formatDateTime), 0);

            //Act
            var result = system.FindAvailableFlights(origin, destination);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestFindAvailableFlightsWithoutAny()
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";

            var system = new SystemManager();
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = String.Format(Error.noFlights, origin, destination);

            //Act
            var result = system.FindAvailableFlights(origin, destination);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestFindAvailableFlightsWithoutValidDestination()
        {
            //Arrange
            var origin = "PLD";

            var system = new SystemManager();
            system.CreateAirport(origin);

            var expected = Error.wrongDestination;

            //Act
            var result = system.FindAvailableFlights(origin, origin);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestBookSeat()
        {
            //Arrange
            const string origin = "PLD";
            const string destination = "PRS";
            const string airlineName = "BGAir";
            const string flightId = "BG14844";
            const int seatClass = 1;
            const int rows = 5;
            const int row = 2;
            const int colms = 5;
            const char colmn = 'C';
            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = $"Seat {row:D3}{colmn} in {seatClass} class is booked for flight from {origin} to {destination} on airline {airlineName}";

            //Act
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestDisplaySystemDetails()
        {
            //Arrange
            var system = new SystemManager();
            const string origin = "PLD";
            const string destination = "PRS";
            const string airlineName = "BGAir";
            const string flightId = "BG14844";

            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = $"Airort aviable 2\r\nWellcome to airport {origin}" +
                           $"\r\nWellcome to airport {destination}\r\nAirline aviable 1\r\n" +
                           $"Airlne {airlineName} offers flights to over 1 destinations\r\n" +
                           $"Flight #{flightId} from {origin} to {destination}.Departure at {DateTime.UtcNow.ToString("MM/dd/yyyy")}\r\nThe flight has 0 section.";

            //Act
            var result = system.DisplaySystemDetails();

            //Assert
            Assert.Equal(expected, result);

        }

    }
}
