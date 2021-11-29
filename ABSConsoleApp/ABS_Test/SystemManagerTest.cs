namespace ABS_xTest
{

    using System;
    using Xunit;

    using ABS_SystemManager;
    using ABS_SystemManager.Models;
    using ABS_SystemManager.DataConstants;
    using System.Text;

    public class SystemManagerTest
    {
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
        [InlineData("PLD")]
        public void CreateAirport(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = String.Format(Success.createdAirport, name);

            //Act
            var result = system.CreateAirport(name);

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
        [InlineData("BGAir")]
        public void CreateAirline(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = String.Format(Success.createdAirline, name);

            //Act
            var result = system.CreateAirline(name);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(8541)]
        [InlineData(10)]
        [InlineData(0)]
        [InlineData(-581415)]
        public void CreateFlight(int days)
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
        public void TestCreateFlightWithSameOrigin()
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "VNA";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

            //Act

            var result = system.CreateFlight(airlineName, origin, origin, year, month, day, id);
            
            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestCreateFlightWithMissingOrigin()
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

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestCreateFlightWithWrongAirline()
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

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }


        [Fact]
        public void TestAddFlightWithSameId()
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

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

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
        public void TestCreateFlightWithWrongId(string id)
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

            var expected = String.Format(Success.createFlight, origin, destination, airlineName);

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void CreateSection()
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
        public void TestCreateSectionWithWrongSeatClass(int rows, int colms, int seatClass)
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

            var expected = String.Format(Success.createFlightSection, (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 4, 1)]
        [InlineData(101, 4, 1)]
        [InlineData(-8, 4, 1)]
        public void TestCreateSectionWithWrongRowsCount(int rows, int colms, int seatClass)
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

            var expected = String.Format(Success.createFlightSection, (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 0, 1)]
        [InlineData(5, 15, 1)]
        [InlineData(10, -4, 1)]
        public void TestCreateSectionWithWrongColumnsCount(int rows, int colms, int seatClass)
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

            var expected = String.Format(Success.createFlightSection, (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // dayTest is date of testing
        // dayFlight is day of flight
        // dayTest must be bigger than dayFlight
        [InlineData(2, 5)]
        [InlineData(5, 15)]
        [InlineData(10, 11)]
        public void TestCreateSectionOnDeparureFlight(int dayFlight, int dayTest)
        {
            //Arrange
            var airlineName = "BGAir";
            var origin = "SFA";
            var destination = "PLD";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(dayFlight);
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
            system.SetTestDate(today.AddDays(dayTest));

            var expected = String.Format(Success.createFlightSection, (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

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

        [Theory]
        [InlineData(1, 2)]
        [InlineData(1, 1)]
        public void TestFindAvailableFlightsWithDepartedFlight(int dayFilight, int dayTest)
        {
            //Arrange
            var origin = "SFA";
            var destination = "PRS";
            var airlineName = "BGAir";
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(dayFilight);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            var id = "BG14844";

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            system.SetTestDate(today.AddDays(dayTest));

            var expected = String.Format(Error.noFlights, origin, destination);

            //Act
            var result = system.FindAvailableFlightsTest(origin, destination);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestBookSeat()
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            const int rows = 5;
            const int colms = 5;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            const int row = 2;
            const char colmn = 'C';

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = String.Format(Success.bookedSeat, $"{row:D3}{colmn}", (SeatClass)seatClass, origin, destination, airlineName);

            //Act
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(5, 5)]
        public void TestBookSeatOnDepartedFlight(int dayFlight, int dayTest)
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            const int rows = 5;
            const int colms = 5;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(dayFlight);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            const int row = 2;
            const char colmn = 'C';

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);
            system.SetTestDate(today.AddDays(dayTest));

            var expected = Error.departedFlight;

            //Act
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestBookSeatAlreadyBooked()
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            const int rows = 5;
            const int colms = 5;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            const int row = 2;
            const char colmn = 'C';

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = Error.bookedSeat;

            //Act
            system.BookSeat(airlineName, flightId, seatClass, row, colmn);
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(2, 101)]
        [InlineData(5, -5)]
        [InlineData(5, 0)]
        public void TestBookSeatWithWrongRow(int rows, int row)
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            const int colms = 5;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            const char colmn = 'C';

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = Error.invalidSeatRow;

            //Act
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 'a')]
        [InlineData(5, 'K')]
        [InlineData(5, 'Е')]
        public void TestBookSeatWithWrongColm(int colms, char colmn)
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            const int rows = 5;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            var row = 1;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = Error.invalidSeatColmn;

            //Act
            var result = system.BookSeat(airlineName, flightId, seatClass, row, colmn);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        // row and colmn must be bigger from rows and colms
        [InlineData(2, 3, 3, 'D')]
        [InlineData(5, 6, 4, 'E')]
        public void TestBookSeatWithWrongSeatNumber(int rows, int row, int colms, char colmn)
        {
            //Arrange
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";
            const int seatClass = 1;
            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;

            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);
            system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            var expected = String.Format(Error.missingItem, "Seat", "number", $"{row:D3}{colmn}");

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
            var origin = "PLD";
            var destination = "PRS";
            var airlineName = "BGAir";
            var flightId = "BG14844";

            var today = DateTime.UtcNow;
            var flightDate = today.AddDays(10);
            var year = flightDate.Year;
            var month = flightDate.Month;
            var day = flightDate.Day;
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            #region Expected
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(DataConstrain.displayAirportsTitle, 2));
            sb.AppendLine(String.Format(DataConstrain.airportToStringTitle, origin));
            sb.AppendLine(String.Format(DataConstrain.airportToStringTitle, destination));
            sb.AppendLine(String.Format(DataConstrain.displayAirlinesTitle, 1));
            sb.AppendLine(String.Format(DataConstrain.airlineToStringTitle, airlineName, String.Format(DataConstrain.airlineWithFlights, 1)));
            sb.AppendLine(String.Format(DataConstrain.flightToStringTitle, flightId, origin, destination, flightDate.ToString(DataConstrain.formatDateTime)));
            sb.AppendLine(String.Format(DataConstrain.flightSectionCount, 0));

            var expected = sb.ToString().Trim();
            #endregion

            //Act
            var result = system.DisplaySystemDetails();

            //Assert
            Assert.Equal(expected, result);

        }

    }
}
