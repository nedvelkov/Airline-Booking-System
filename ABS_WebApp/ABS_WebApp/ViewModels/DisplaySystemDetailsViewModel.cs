using System.Collections.Generic;

namespace ABS_WebApp.ViewModels
{
    public class DisplaySystemDetailsViewModel
    {
        public DisplaySystemDetailsViewModel() => Details = new List<string>();
        public List<string> Details { get; set; }
    }
}
