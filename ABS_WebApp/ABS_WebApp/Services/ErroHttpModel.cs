namespace ABS_WebApp.Services
{
    public class ErroHttpModel<T>
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public int Status { get; set; }

        public string TraceId { get; set; }

        public T Errors { get; set; }
    }
}
