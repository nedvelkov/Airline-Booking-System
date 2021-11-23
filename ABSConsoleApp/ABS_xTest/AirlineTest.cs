namespace ABS_xTest
{

    using System;
    using System.Linq;


    using Xunit;
    using Models;

    using static Mocks.MockABS;
    using Models.Contracts;

    public class AirlineTest
    {
        const string flightId = "testFlight";
        IFlight flight = FlightMock(flightId);

        [Theory]
        [InlineData("BGAir")]
        [InlineData("TRAir")]
        [InlineData("UKAir")]
        [InlineData("BCD")]
        public void CreateValidAirline(string name)
        {
            //Arrange

            //Act
            var airline = new Airline(name);
            //Asert
            Assert.IsType<Airline>(airline);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("RayanAir")]
        [InlineData("   ")]
        [InlineData("")]
        [InlineData("Lufthansa")]
        public void CreateInvalidAirlineWithWrongLength(string name)
        {
            //Arrange
            var expected = "Name of airline must be between 1 and 5 characters";
            //Act
            try
            {
                var airline = new Airline(name);
            }
            catch (Exception a)
            {
                //Asert
                Assert.Equal(expected, a.Message);
            }
        }

       [Theory]
       [InlineData("AW dw")]
       [InlineData("A  a")]
       [InlineData("A+wWa")]
        public void CreateInvalidAirlineWith(string name)
        {
            //Arrange
            var expected = "Name of airline must have only letters";
            //Act
            try
            {
                var airline = new Airline(name);
            }
            catch (Exception a)
            {
                //Asert
                Assert.Equal(expected, a.Message);
            }
        }


        [Fact]
        public void TestFligthsListInNewAirline()
        {
            //Arrange
            var airline = new Airline("test");
            var expected = 0;
            //Act
            var result = airline.Flights.Count;
            //Asert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreateAirlineWithFlights()
        {
            //Arrange
            var airline = new Airline("test");
            //Act
            airline.AddFlight(this.flight);
            var result = airline.Flights.First().Id;
            //Asert
            Assert.Equal(flightId, result);
        }

        [Fact]
        public void TestAddFlightsToAirlineWithSameId()
        {
            //Arrange
            var airline = new Airline("test");
            var expected = $"Flight with id:{flightId} already exist";
            var expectedCount = 1;
            //Act
            airline.AddFlight(this.flight);
            try
            {
                airline.AddFlight(this.flight);
            }
            catch (Exception a)
            {
                //Asert
                Assert.Equal(expected, a.Message);
                Assert.Equal(expectedCount, airline.Flights.Count);
            }
        }
    }
}
