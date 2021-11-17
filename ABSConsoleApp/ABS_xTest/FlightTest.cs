namespace ABS_xTest
{
    using System;
    using System.Linq;

    using Xunit;
    using Models;
    using Models.Enums;
    public class FlightTest
    {
        [Theory]
        [InlineData("SFA", "VRN", 5,"SAI159")]
        [InlineData("SFA", "PLD", 28541, "SAI270")]
        [InlineData("SFA", "PRG", 0, "SAI311")]
        public void CreateValidFlight(string org,string dest,double days, string id)
        {
            //Arrange
            var origin = new Airport(org);
            var destination = new Airport(dest);
            var today = DateTime.UtcNow.Date;
            var date = today.AddDays(days);
            //Act
            var flight = new Flight(origin,destination,date,id);
            //Asert
            Assert.IsType<Flight>(flight);

        }
        [Theory]
        [InlineData("SFA", "VRN",-5, "SAI159")]
        [InlineData("VRN", "PLD",-8, "SAI270")]
        [InlineData("PRS", "PRG",-105, "SAI311")]
        public void CreateInvalidFlightWithWrongDate(string org, string dest,double days, string id)
        {
            //Arrange
            var origin = new Airport(org);
            var destination = new Airport(dest);
            var today = DateTime.UtcNow.Date;
            var date = today.AddDays(days);
            var expected = "Date is not valid.";
            //Act
            string result = null;
            try
            {
             var flight = new Flight(origin, destination, date, id);
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData("SFA", "SFA", 5, "SAI159")]
        [InlineData("PLD", "PLD", -5, "SAI270")]
        public void CreateInvalidFlightWithWrongDestination(string org, string dest, double days, string id)
        {
            //Arrange
            var origin = new Airport(org);
            var destination = new Airport(dest);
            var today = DateTime.UtcNow.Date;
            var date = today.AddDays(days);
            var expected = "Destionation must be different from origin.";
            //Act
            string result = null;
            try
            {
                var flight = new Flight(origin, destination, date, id);
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected, result);

        }

        [Fact]
        public void TestFlightSeactionInNewFlight()
        {
            //Arrange
            var origin = new Airport("SFA");
            var destination = new Airport("VRN");
            var today = DateTime.UtcNow.Date;
            var date = today.AddDays(8);
            var id = "SAI311";
            var flight = new Flight(origin, destination, date, id);
            var expected = 0;
            //Act
            var result = flight.FlightSections.Count;
            //Asert
            Assert.Equal(expected, result);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void AddFlightSeactionInFlight(int seatClass)
        {
            //Arrange
            var origin = new Airport("SFA");
            var destination = new Airport("VRN");
            var today = DateTime.UtcNow.Date;
            var date = today.AddDays(8);
            var id = "SAI311";
            var flight = new Flight(origin, destination, date, id);
            var expectedSeatClassCount = 1;
            var expectedSeatCount = 5*8;
            //Act
            flight.AddFlightSection((SeatClass)seatClass, 5, 8);
            var seatClassCount = flight.FlightSections.Count;
            var seatCount = flight.FlightSections.First().Seats.Count;
            //Asert
            Assert.Equal(expectedSeatClassCount, seatClassCount);
            Assert.Equal(expectedSeatCount, seatCount);
        }
    }
}
