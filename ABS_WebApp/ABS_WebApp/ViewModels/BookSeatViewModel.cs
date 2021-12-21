using System.Collections.Generic;

using ABS_Models;

namespace ABS_WebApp.ViewModels
{
    public class BookSeatViewModel
    {
        public BookSeatViewModel()
        {
            Flights = new();
            Airlines = new();
            Seat = new BookSeatModel();
        }

        public BookSeatModel Seat { get; set; }

        public List<string> Flights { get; set; }

        public List<string> Airlines { get; set; }
    }
}