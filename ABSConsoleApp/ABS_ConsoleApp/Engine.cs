namespace ABSConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ABS_SystemManager.Interfaces;

    using ABS_SystemManager;
    using ABS_SystemManager.DataConstants;
    public class Engine
    {
        private ISystemManager _manager;
        private List<string> _types = new List<string> { "Airport", "Airlane", "Flight", "Flight section" };
        private bool _main = true;
        public Engine() => _manager = new SystemManager();

        public void Run()
        {
            Console.SetWindowSize(120, 30);
            var flag = true;

            while (flag)
            {
                if (_main)
                {
                MainMenu();
                    _main = false;
                }
                var command = Console.ReadLine();
                switch (command.ToLower())
                {
                    case "-create":
                        Console.WriteLine(Create());
                        SelectCreateType();
                        break;
                    case "-find":
                        Find();
                        break;
                    case "-book":
                        Book();
                        break;
                    case "-details":
                        Details();
                        break;
                    case "--main":
                        Run();
                        break;
                    case "--help":
                        HelpMenu();
                        break;
                    case "--exit":
                        flag = false;
                        break;
                }
            }
            Environment.Exit(0);
        }

        private string Create()
        {
            Console.Clear();
            Console.Title = DataConstrain.titleConsole + "-Create";
            var sb = new StringBuilder();
            sb.AppendLine("Select what type want to create:");
            _types.ForEach(x => sb.AppendLine($" -{x}"));
            return sb.ToString().Trim();
        }

        private void SelectCreateType()
        {
            var rowPosition = Console.CursorTop;

            var infoRow = rowPosition + 2;
            var flag = true;
            Console.SetCursorPosition(0, rowPosition);
            while (flag)
            {
                var key = Console.ReadKey();
                var curentRow = Console.CursorTop;

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        flag = false;
                        break;

                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(0, CursorUp());
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(0, CursorDown());
                        break;

                    case ConsoleKey.Enter:
                        var action = curentRow - 1 >= 0 && curentRow - 1 < _types.Count ? _types[curentRow - 1] : "";
                        Console.SetCursorPosition(0, infoRow);
                        switch (action)
                        {
                            case "Airport":
                                Airport();
                                flag = false;
                                break;
                            case "Airlane":
                                Airline();
                                flag = false;
                                break;
                            case "Flight":
                                Flight();
                                flag = false;
                                break;
                            case "Flight section":
                                FlightSection();
                                flag = false;
                                break;
                        }
                        break;
                    case ConsoleKey.F1:
                        Console.SetCursorPosition(0, infoRow);
                        Console.WriteLine(DataConstrain.helpCommand);
                        infoRow = Console.CursorTop;
                        Console.SetCursorPosition(0, rowPosition);
                        break;
                    default:
                        break;
                }
            }

        }

        private void Airline()
        {
            Console.Title = DataConstrain.titleConsole + "-Create Airline";
            Console.WriteLine(Error.airlineName);
            Console.WriteLine("Enter name for airline:");
            var flag = true;
            while (flag)
            {
                Console.Write("Name:");
                var name = Console.ReadLine();

                var message = _manager.CreateAirline(name);
                Console.WriteLine(message);

                flag = BreakCicle(message, "successfully");

            }
        }

        private void Airport()
        {
            Console.Title = DataConstrain.titleConsole + "-Create Airport";
            Console.WriteLine(Error.airportName);
            var flag = true;
            while (flag)
            {
                Console.WriteLine("Enter name for aiport:");
                Console.Write("Name:");
                var name = Console.ReadLine();

                var message = _manager.CreateAirport(name);
                Console.WriteLine(message);

                flag = BreakCicle(message, "successfully");
            }
        }

        private void Flight()
        {
            Console.Title = DataConstrain.titleConsole + "-Create Flight";

            var flag = true;
            while (flag)
            {
                Console.Write("Airline associate with flight:");
                var airlineName = Console.ReadLine();

                Console.Write("Origin of flight (airport name):");
                var origin = Console.ReadLine();

                Console.Write("Destination point of flight (airport name):");
                var destination = Console.ReadLine();

                Console.Write("Year of flight:");
                var year = ParseString("Year of flight:");

                Console.Write("Month of flight:");
                var month = ParseString("Month of flight:");

                Console.Write("Day of flight:");
                var day = ParseString("Day of flight:");

                Console.Write("Flight identification number:");
                var id = Console.ReadLine();

                var message = _manager.CreateFlight(airlineName, origin, destination, year, month, day, id);
                Console.WriteLine(message);

                flag = BreakCicle(message, "successfully");

            }
        }

        private void FlightSection()
        {
            Console.Title = DataConstrain.titleConsole + "-Create flight section";

            var flag = true;
            while (flag)
            {
                var seatClass = SelectSection();
                Console.Write("Airline associate with flight:");
                var airlineName = Console.ReadLine();

                Console.Write("Flight identification number:");
                var id = Console.ReadLine();

                Console.Write("Rows of section:");
                var rows = ParseString("Rows of section:");

                Console.Write("Columns of section:");
                var colms = ParseString("Month of flight:");

                var message = _manager.CreateSection(airlineName, id, rows, colms, seatClass);
                Console.WriteLine(message);

                flag = BreakCicle(message, "successfully");
            }
        }

        private void Book()
        {
            Console.Title = DataConstrain.titleConsole + "-Book seat";

            var flag = true;
            while (flag)
            {
                Console.Clear();
                var seatClass = SelectSection();
                if (seatClass == DataConstrain.breakVoid)
                {
                    return;
                }

                Console.Write("Airline associate with flight:");
                var airlineName = Console.ReadLine();

                Console.Write("Flight identification number:");
                var id = Console.ReadLine();

                Console.Write("Rows of section:");
                var row = ParseString("Row of section:");

                Console.Write("Column of section:");
                var colmn = Console.ReadLine();

                var message = _manager.BookSeat(airlineName, id, seatClass, row, colmn[0]);
                Console.WriteLine(message);

                flag = BreakCicle(message, "successfully");
            }
        }
        private void Details()
        {
            Console.Clear();
            Console.WriteLine(_manager.DisplaySystemDetails());
        }

        private void Find()
        {
            Console.Title = DataConstrain.titleConsole + "-Find flight";

            var flag = true;
            while (flag)
            {
                Console.Write("Origin of flight (airport name):");
                var origin = Console.ReadLine();

                Console.Write("Destination point of flight (airport name):");
                var destination = Console.ReadLine();

                _manager.FindAvailableFlights(origin, destination);

                var message = _manager.FindAvailableFlights(origin, destination);
                Console.WriteLine(message);

                flag = BreakCicle(message, "Departure");
            }

        }
        private void HelpMenu()
        {
            Console.Clear();
            Console.WriteLine(DataConstrain.helpCommand);
        }

        private int SelectSection()
        {
            Console.Clear();
            Console.WriteLine("Select type of section:");
            var sectionTypes = new List<string>() { " -First class", " -Business class", " -Economy class" };
            sectionTypes.ForEach(x => Console.WriteLine(x));
            var orgPossition = Console.CursorTop;
            while (true)
            {
                var key = Console.ReadKey();
                var curentRow = Console.CursorTop;

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        return DataConstrain.breakVoid;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(0, CursorUp());
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(0, CursorDown());
                        Console.ResetColor();
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, orgPossition + 2);
                        return curentRow;
                    default:
                        break;
                }
            }
        }

        private int ParseString(string text)
        {
            while (true)
            {
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a valid number");
                    Console.Write(text);
                }
            }
        }
        private void MainMenu()
        {
            Console.Title = DataConstrain.titleConsole;
            var greeting = "Welcome to ABS - Airline Bookig System!";
            Console.Clear();
            Console.WriteLine(greeting);
            Console.WriteLine(DataConstrain.helpCommand);
        }

        private bool ReturnToMainMenu()
        {
            Console.Write(DataConstrain.returnToMainMenu);
            var key = Console.ReadKey();
            Console.WriteLine("");
            switch (key.Key)
            {
                case ConsoleKey.Y:
                case ConsoleKey.Escape:
                    _main = true;
                    return false;
                default:
                    return true;
            }
        }

        private bool BreakCicle(string message, string word)
        {
            if (message.Contains(word))
            {
                return false;
            }
            return ReturnToMainMenu();
        }

        private int CursorUp()
            => Console.CursorTop == 0 ? 0 : Console.CursorTop - 1;
        private int CursorDown()
            => Console.CursorTop == _types.Count ? Console.CursorTop : Console.CursorTop + 1;
    }
}
