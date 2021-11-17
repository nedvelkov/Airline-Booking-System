namespace ABS_xTest
{
    using System;
    using System.Linq;

    using Xunit;
    using Models;
    using Models.Enums;

    public class FlightSectionTest
    {
        [Theory]
        [InlineData((SeatClass)1,1,10)]
        [InlineData((SeatClass)2,100,5)]
        [InlineData((SeatClass)3,50,3)]
        public void CreateValidFlightSection(SeatClass seatClass,int rows,int colms)
        {
            //Arrange
            var expectedRow = rows;
            var expectedColmn = (char)(64+colms);
            var expectedSeats = rows*colms;
            //Act
            var flightSection = new FlightSection(seatClass,rows,colms);
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
        public void CreateInvalidFlightSectionWithWrongSeatClass(SeatClass seatClass, int rows, int colms)
        {
            //Arrange
            var expected = "Seat class is not valid.";
            
            //Act
            string result = null;
            try
            {
            var flightSection = new FlightSection(seatClass, rows, colms);
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected,result);
        }

        [Theory]
        [InlineData((SeatClass)2, -1, 10)]
        [InlineData((SeatClass)1, 985, 5)]
        public void CreateInvalidFlightSectionWithWrongRows(SeatClass seatClass, int rows, int colms)
        {
            //Arrange
            var expected = "Specified argument was out of the range of valid values. (Parameter 'Rows of seat must be between 1 and 100')";
     
            //Act
            string result = null;
            try
            {
                var flightSection = new FlightSection(seatClass, rows, colms);
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData((SeatClass)2, 20, -5)]
        [InlineData((SeatClass)1, 98, 15)]
        public void CreateInvalidFlightSectionWithWrongColms(SeatClass seatClass, int rows, int colms)
        {
            //Arrange
            var expected = "Specified argument was out of the range of valid values. (Parameter 'Columns of seat must be between 1 and 10')";

            //Act
            string result = null;
            try
            {
                var flightSection = new FlightSection(seatClass, rows, colms);
            }
            catch (Exception a)
            {
                result = a.Message;
            }

            //Asert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestHasAviableSeatInFlightSection()
        {
            //Arrange
            var flightSection = new FlightSection(SeatClass.business, 5, 5);
            //Act
            var result = flightSection.HasAvaibleSeats();
            //Asert
            Assert.True(result);
        }

        [Fact]
        public void TestHasAviableSeatInFlightSectionWithAllSeatBooked()
        {
            //Arrange
            var flightSection = new FlightSection(SeatClass.business, 5, 5);
            foreach (var item in flightSection.Seats)
            {
                item.BookSeat();
            }
            //Act
            var result = flightSection.HasAvaibleSeats();
            //Asert
            Assert.False(result);
        }


        [Theory]
        [InlineData (4, 'B')]
        [InlineData(2, 'D')]
        public void BookFreeSeat(int row, char colmn)
        {
            //Arrange
            var flightSection = new FlightSection(SeatClass.business, 5, 5);
            //Act
            flightSection.BookSeat(row, colmn);
            var bookSeat = flightSection.Seats.FirstOrDefault(x => x.Row == row && x.Colmn == colmn);
            //Asert
            Assert.True(bookSeat.Booked);
        }


        [Theory]
        [InlineData(4, 'B')]
        [InlineData(2, 'D')]
        public void TestBookSeatOnBookedSeat(int row, char colmn)
        {
            //Arrange
            var expected = "This seat is already booked";
            var flightSection = new FlightSection(SeatClass.business, 5, 5);
            foreach (var item in flightSection.Seats)
            {
                item.BookSeat();
            }
            //Act
            string result = null;
            try
            {
                flightSection.BookSeat(row, colmn);

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
