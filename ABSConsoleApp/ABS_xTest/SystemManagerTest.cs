namespace ABS_xTest
{

    using Facade;
    using System;
    using Xunit;

    public class SystemManagerTest
    {
        [Theory]
        [InlineData("SVA")]
        [InlineData("PLD")]
        [InlineData("VRN")]
        public void CreateValidAirport(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = $"Airport {name} is created successfully";
            //Act
            var result = system.CreateAirport(name);
            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("SA")]
        [InlineData("PLOD")]
        [InlineData("")]
        public void CreateInvalidAirport(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = "Airport name must be 3 characters in length";
            //Act

            var result = system.CreateAirport(name);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("BGAir")]
        [InlineData("TRAir")]
        [InlineData("UKAir")]
        public void CreateValidAirline(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = $"Airline { name} is created successfully";
            //Act
            var result = system.CreateAirline(name);
            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("RayanAir")]
        [InlineData("   ")]
        public void CreateInvalidAirline(string name)
        {
            //Arrange
            var system = new SystemManager();
            var expected = "Name of airline must be between 1 and 5 characters";
            //Act

            var result = system.CreateAirline(name);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BGAir", "SFA", "VRN", 2021, 12, 5, "BG14844")]
        public void CreateValidFlight(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = $"Flight from {origin} to {destination} on airline {airlineName} is created successfully";

            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BGAir", "SFA", "SFA", 2021, 12, 5, "BG14844")]
        public void CreateInvalidFlightWithSameOrigin(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);

            var expected = $"Destionation must be different from origin";

            //Act

            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            ;
            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("BGAir", "SFA", "SFA", 2021, 12, 5, "BG14844")]
        public void CreateInvalidFlightWithMissingOrigin(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();
            system.CreateAirline(airlineName);

            var expected = $"Airport with name {origin} don't exist";
            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("BGAir", "SFA", "SFA", 2021, 12, 5, "BG14844")]
        public void CreateInvalidFlightWithWrongAirline(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();

            var expected = $"Airline with name {airlineName} don't exist";
            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("BGAir", "SFA", "SFA", 2021, 18, 5, "BG14844")]
        public void CreateInvalidFlightWithWrongDate(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = "Date is not valid";
            //Act
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            ;
            //Assert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("BGAir", "SFA", "VRN", 2021, 12, 5, "BG14844")]
        public void AddFlightWithSameId(string airlineName, string origin, string destination, int year, int month, int day, string id)
        {
            //Arrange
            var system = new SystemManager();
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = $"Flight with id:{id} already exist";

            //Act
            system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            var result = system.CreateFlight(airlineName, origin, destination, year, month, day, id);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BGAir", "BG14844",2,4,1)]
        public void CreateValidSection(string airlineName, string flightId, int rows, int colms, int seatClass)
        {
            //Arrange
            var system = new SystemManager();
            var origin = "SFA";
            var destination = "PLD";
            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = $"Section {(SeatClass)seatClass} class is created successfully for flight from {origin} to {destination} on airline {airlineName}";


            //Act
            var result = system.CreateSection(airlineName,flightId,rows,colms,seatClass);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("BGAir", "BG14844", 2, 4, 5)]
        public void CreateInvalidSection(string airlineName, string flightId, int rows, int colms, int seatClass)
        {
            //Arrange
            var system = new SystemManager();
            var origin = "SFA";
            var destination = "PLD";
            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);
            system.CreateFlight(airlineName, origin, destination, year, month, day, flightId);

            var expected = "Seat class is not valid";


            //Act
            var result = system.CreateSection(airlineName, flightId, rows, colms, seatClass);

            //Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void TestFindAvailableFlightsAtleastOne()
        {
            //Arrange
            var system = new SystemManager();
            const string origin = "SFA";
            const string destination = "PRS";
            const string airlineName = "BGAir";
            system.CreateAirline(airlineName);
            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var year = DateTime.UtcNow.Year;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Day;
            var id = "BG14844";
            system.CreateFlight(airlineName, origin, destination, year, month, day, id);
            var expected = $"Flight #BG14844 from SFA to PRS.Departure at {DateTime.UtcNow.ToString("MM/dd/yyyy")}\r\nThe flight has 0 section.";

            //Act
            var result = system.FindAvailableFlights(origin, destination);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestFindAvailableFlightsWithoutAny()
        {
            //Arrange
            var system = new SystemManager();
            const string origin = "PLD";
            const string destination = "PRS";

            system.CreateAirport(origin);
            system.CreateAirport(destination);

            var expected = $"There is no flight from {origin} to {destination}, at this time";

            //Act
            var result = system.FindAvailableFlights(origin, destination);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestFindAvailableFlightsWithoutValidDestination()
        {
            //Arrange
            var system = new SystemManager();
            const string origin = "PLD";
            system.CreateAirport(origin);

            var expected = "Origin point can't be same as destination";

            //Act
            var result = system.FindAvailableFlights(origin, origin);

            //Assert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestBookSeat()
        {
            //Arrange
            var system = new SystemManager();
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
