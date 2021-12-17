namespace ABS_WebApp.Services.RequestModels
{
    public class SectionRequestModel
    {
        public string Id { get; set; }

        public string AirlineName { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        public int SeatClass { get; set; }
    }
}
