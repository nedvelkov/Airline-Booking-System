namespace ABSConsoleApp.Models
{
    using System;
    using ABSConsoleApp.Contracts;
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
             Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
