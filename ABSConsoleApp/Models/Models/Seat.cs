namespace Models
{
    using Models.Interfaces;
    using System;

    public class Seat:ISeat
    {
        private int row;
        private char colmn;
        private bool booked;

        public Seat(int row,char colmn)
        {
            this.Row = row;
            this.Colmn = colmn;
        }
        public int Row
        {
            get { return this.row; }
           private set
            {
                if (value <= 0 || value > 100)
                {
                    throw new IndexOutOfRangeException("Invalid seat row");
                }
                this.row = value;
            }
        }

        public char Colmn
        {
            get { return this.colmn; }
           private set
            {
                var colmUpperValue = Char.ToUpper(value);
                if(colmUpperValue<'A' || colmUpperValue > 'J')
                {
                    throw new IndexOutOfRangeException("Invalid seat colmn");
                }
                this.colmn = colmUpperValue;
            }
        }

        public bool Booked => this.booked;

        public void BookSeat()
        {
            if(this.booked)
            {
                throw new Exception("This seat is already booked");
            }
            this.booked = true;
        }

        public override string ToString()
        {
            var seatIsBooked = this.Booked ? "booked" : "free";
            var text = $" {this.Row.ToString("D3")}{this.Colmn} - {seatIsBooked}";
            return text;
        }
    }
}
