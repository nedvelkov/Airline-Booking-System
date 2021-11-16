namespace ABS_xTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;
    using Models;
    using Models.Enums;

    public class FlightSectionTest
    {
        [Theory]
        [InlineData((SeatClass)1,1,10)]
        [InlineData((SeatClass)2,100,5)]
        [InlineData((SeatClass)3,50,3)]
        public void CreateValidFlightSection(SeatClass seatClass,int rows,int colmns)
        {
            //Arrange
            var expectedRow = rows;
            var expectedColmn = (char)(64+colmns);
            var expectedSeats = rows*colmns;
            //Act
            var flightSection = new FlightSection(seatClass,rows,colmns);
            var lastSeat = flightSection.Seats.Last();
            //Asert
            Assert.IsType<FlightSection>(flightSection);
            Assert.Equal(expectedRow, lastSeat.Row);
            Assert.Equal(expectedColmn, lastSeat.Colmn);
            Assert.Equal(expectedSeats, flightSection.Seats.Count);
        }

        [Theory]
        [InlineData((SeatClass)5, 1, 10)]
        [InlineData((SeatClass)0, 3, 5)]
        public void CreateInValidFlightSectionWithWrongSeatClass(SeatClass seatClass, int rows, int colmns)
        {
            //Arrange
            var expected = "Seat class is not valid.";
            ;
            //Act
            string result = null;
            try
            {
            var flightSection = new FlightSection(seatClass, rows, colmns);
                ;
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected,result);
        }

    }
}
