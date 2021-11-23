namespace ABSConsoleApp
{
    using ABSConsoleApp.Models;
    class Program
    {
        static void Main()
        {
            var reader = new ConsoleReader();
            var writer = new ConsoleWriter();
            var engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
