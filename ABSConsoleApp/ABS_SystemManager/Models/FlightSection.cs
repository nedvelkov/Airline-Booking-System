﻿using System;
using System.Linq;
using System.Text;

using ABS_SystemManager.Interfaces;
using static ABS_DataConstants.DataConstrain;

namespace ABS_SystemManager.Models
{

    internal class FlightSection : IFlightSection
    {

        private ISeat[,] _seats;

        public SeatClass SeatClass { get; set; }

        public ISeat[,] Seats => _seats;

        public bool HasAvaibleSeats()
        {
            var listOfSeats = _seats.Cast<ISeat>().ToList();
            return listOfSeats.Any(x => x.Booked);
        }

        public void BookSeat(int row, int column) => _seats[row, column].BookSeat();

        public void AddSeats(ISeat[,] seats) => _seats = seats;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(String.Format(flightSectionToStringTitle, SeatClass, _seats.Length));
            var listOfSeats = _seats.Cast<ISeat>().ToList();
            listOfSeats.ForEach(x => sb.AppendLine(x.ToString()));

            return sb.ToString().TrimEnd();
        }

    }
}
