namespace ABS_xTest
{

    using System;
    using System.Linq;


    using Xunit;
    using Models;
    public class AirlineTest
    {
        [Theory]
        [InlineData("BGAir")]
        [InlineData("TRAir")]
        [InlineData("UKAir")]
        [InlineData("A  a")] //Not good name
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
        public void CreateInvalidAirline(string name)
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

        [Theory]
        [InlineData("SF108")]
        [InlineData("RA854")]
        [InlineData("TT749")]
        [InlineData("TT049")]
        public void CreateAirlineWithFlights(string id)
        {
            //Arrange
            var airline = new Airline("test");
            var orig = new Airport("SFA");
            var dest = new Airport("VRN");
            var date = DateTime.UtcNow.Date;
            var flight = new Flight(orig, dest, date, id);
            //Act
            airline.AddFlight(flight);
            var result = airline.Flights.First().Id;
            //Asert
            Assert.Equal(id, result);
        }

        [Theory]
        [InlineData("SF108")]
        [InlineData("RA854")]
        [InlineData("TT749")]
        [InlineData("TT049")]
        public void TestAddFlightsToAirlineWithSameId(string id)
        {
            //Arrange
            var airline = new Airline("test");
            var orig = new Airport("SFA");
            var dest = new Airport("VRN");
            var date = DateTime.UtcNow.Date;
            var flight = new Flight(orig, dest, date, id);
            var expected = $"Flight with id:{id} already exist";
            //Act
            airline.AddFlight(flight);
            try
            {
                airline.AddFlight(flight);
            }
            catch (Exception a)
            {
                //Asert
                Assert.Equal(expected, a.Message);
            }
        }
    }
}
