namespace ABS_xTest
{

    using Xunit;
    using System;
    using Models;

    public class SeatTest
    {
        [Theory]
        [InlineData(1, 'J')]
        [InlineData(100, 'A')]
        [InlineData(25, 'D')]
        public void CreateValidSeat(int row, char colmn)
        {
            //Act
            var seat = new Seat(row, colmn);
            //Asert
            Assert.IsType<Seat>(seat);
        }

        [Theory]
        [InlineData('K')]
        [InlineData('Z')]
        [InlineData('Î')]
        public void CreateInvalidSeatWithWrongColmn(char colmn)
        {
            //Arrange
            var expected = "Invalid seat colmn";
            //Act
            string result = null;
            try
            {
                var seat = new Seat(1, colmn);
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(958)]
        [InlineData(888)]
        public void CreateInvalidSeatWithWrongRow(int row)
        {
            //Arrange
            var expected = "Invalid seat row";
            //Act
            string result = null;
            try
            {
                var seat = new Seat(row, 'J');
            }
            catch (Exception a)
            {
                result = a.Message;
            }
            //Asert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(1, 'J')]
        [InlineData(100, 'A')]
        [InlineData(25, 'D')]
        public void ValidateBoockingSeat(int row, char colmn)
        {
            //Arrange
            var seat = new Seat(row, colmn);
            //Act
            seat.BookSeat();
            //Asert
            Assert.True(seat.Booked);
        }

        [Theory]
        [InlineData(1, 'J')]
        [InlineData(100, 'A')]
        [InlineData(25, 'D')]
        public void ValidateErrorAtBoockingSeat(int row, char colmn)
        {
            //Arrange
            var seat = new Seat(row, colmn);
            var expected = "This seat is already booked";
            //Act
            seat.BookSeat();
            //Asert
            try
            {
                seat.BookSeat();
            }
            catch (Exception a)
            {

                Assert.Equal(expected, a.Message);
            }
        }
    }
}
