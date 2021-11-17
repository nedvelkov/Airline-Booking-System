namespace ABS_xTest
{

    using System;

    using Xunit;
    using Models;
    public class AirportTest
    {
        [Theory]
        [InlineData("SFA")]
        [InlineData("VRN")]
        [InlineData("PLD")]
        public void CreateValidAirport(string name)
        {
            //Arrange

            //Act
            var airport = new Airport(name);
            //Asert
            Assert.IsType<Airport>(airport);
        }
        [Theory]
        [InlineData("SA")]
        [InlineData("VRNaa")]
        [InlineData(null)]
        public void CreateInvalidAirport(string name)
        {
            //Arrange
            var expected = "Airport name must be 3 characters in length";
            //Act
            string result = null;
            try
            {
                var airport = new Airport(name);
            }
            catch (Exception a)
            {
                result = a.Message;
                ;
            }
            //Asert
            Assert.Equal(expected, result);
        }
    }
}
