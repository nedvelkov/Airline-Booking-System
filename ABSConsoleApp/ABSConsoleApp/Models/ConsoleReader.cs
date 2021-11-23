namespace ABSConsoleApp.Models
{
    using System;
    using Contracts;
    public class ConsoleReader : IReader
    {

        public string ReadLine()
        {
            return Console.ReadLine();
        }

    }
}
