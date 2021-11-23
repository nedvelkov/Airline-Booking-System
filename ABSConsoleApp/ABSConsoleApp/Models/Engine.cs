namespace ABSConsoleApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Contracts;
    using Facade;
    using UI;
    public class Engine
    {
        private IReader reader;
        private IWriter writer;
        private ISystemManager manager;
        private List<string> types = new List<string> { "Airport", "Airlane", "Flight", "Flight section" };
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.manager = new SystemManager();
        }

        public void Run()
        {
            SetConsoleSize();
            ConsoleSettings.Title = "ABS - Airline Bookig System";
            var greeting = "Welcome to ABS - Airline Bookig System!";
            ConsoleSettings.Clear();
            writer.WriteLine(greeting);
            writer.WriteLine(Help());
            var flag = true;
            while (flag)
            {
                var command = reader.ReadLine();
                switch (command.ToLower())
                {
                    case "-create":
                        writer.WriteLine(Create());
                        SelectCreateType();
                        break;
                    case "-find":
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

        private string Help()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Create object command: -create");
            sb.AppendLine("Find flight command: -find");
            sb.AppendLine("Book seat command: -book");
            sb.AppendLine("Display system detail command: -details");
            sb.AppendLine("Helm menu command: --help");
            sb.AppendLine("Go to main menu command: --main");
            sb.AppendLine("Close program command: --exit");
            sb.AppendLine("Cansel current operation and return main menu press \"esc\" button");

            return sb.ToString().Trim();
        }

        private string Create()
        {
            var title = ConsoleSettings.Title;
            ConsoleSettings.Clear();
            ConsoleSettings.Title = title + "-Create";
            var sb = new StringBuilder();
            sb.AppendLine("Select what type want to create:");
            types.ForEach(x => sb.AppendLine($" -{x}"));
            return sb.ToString().Trim();
        }

        private void SelectCreateType()
        {
            var rowPosition = ConsoleSettings.ConsolePosstionRow();

            var infoRow = rowPosition + 2;
            var flag = true;
            var main = false;
            ConsoleSettings.SetPossition(0, rowPosition);
            while (flag)
            {
                var key = ConsoleSettings.Key;
                var curentRow = ConsoleSettings.ConsolePosstionRow();

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        flag = false;
                        main = true;
                        break;

                    case ConsoleKey.UpArrow:
                        ConsoleSettings.SetPossition(0, curentRow - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        ConsoleSettings.SetPossition(0, curentRow + 1);
                        ConsoleSettings.ResetColor();
                        break;
                    case ConsoleKey.Enter:
                        var action = curentRow - 1 >= 0 && curentRow - 1 < types.Count ? types[curentRow - 1] : "";
                        ConsoleSettings.SetPossition(0, infoRow);
                        switch (action)
                        {
                            case "Airport":
                                main = Airport();
                                flag = false;
                                break;
                            case "Airlane":
                                main = Airline();
                                flag = false;
                                break;
                            case "Flight":
                                main = Flight();
                                flag = false;
                                break;
                            case "Flight section":
                                main = FlightSection();
                                flag = false;
                                break;
                        }
                        break;
                    case ConsoleKey.F1:
                        ConsoleSettings.SetPossition(0, infoRow);
                        writer.WriteLine(Help());
                        infoRow = ConsoleSettings.ConsolePosstionRow();
                        ConsoleSettings.SetPossition(0, rowPosition);
                        break;
                    default:
                        break;
                }
            }
            if (main)
            {
                Run();
            }
        }

        private bool Airline()
        {
            writer.WriteLine("Airline name must be between 1 and 5 letters");
            writer.WriteLine("Enter name for airline:");
            var flag = true;
            var main = false;
            while (flag)
            {
                writer.Write("Name:");
                var name = reader.ReadLine();
                var message = this.manager.CreateAirline(name);
                writer.WriteLine(message);
                if (message.Contains("successfully") == false)
                {
                    writer.Write("Do you want to return in main menu (Y/N): ");
                    flag = ReurnToMainMenu();
                    main = !flag;
                    writer.WriteLine("");

                }
                else
                {
                    flag = false;
                }
            }
            return main;
        }

        private bool Airport()
        {
            writer.WriteLine("Airport name must be 3 upper letters");
            var flag = true;
            var main = false;
            while (flag)
            {
                writer.WriteLine("Enter name for aiport:");
                writer.Write("Name:");
                var name = reader.ReadLine();
                var message = this.manager.CreateAirport(name);
                writer.WriteLine(message);
                if (message.Contains("successfully") == false)
                {
                    writer.Write("Do you want to return in main menu (Y/N): ");
                    flag = ReurnToMainMenu();
                    main = !flag;
                    writer.WriteLine("");
                }
                else
                {
                    flag = false;
                }
            }
            return main;
        }

        private bool Flight()
        {
            var flag = true;
            var main = false;
            while (flag)
            {
                writer.Write("Airline associate with flight:");
                var airlineName = reader.ReadLine();
                writer.Write("Origin of flight (airport name):");
                var origin = reader.ReadLine();
                writer.Write("Destination point of flight (airport name):");
                var destination = reader.ReadLine();
                writer.Write("Year of flight:");
                var year = ParseString("Year of flight:");
                writer.Write("Month of flight:");
                var month = ParseString("Month of flight:");
                writer.Write("Day of flight:");
                var day = ParseString("Day of flight:");
                writer.Write("Flight identification number:");
                var id = reader.ReadLine();

                var message = this.manager.CreateFlight(airlineName, origin, destination, year, month, day, id);
                writer.WriteLine(message);
                if (message.Contains("successfully") == false)
                {
                    writer.Write("Do you want to return in main menu (Y/N): ");
                    flag = ReurnToMainMenu();
                    main = !flag;
                    writer.WriteLine("");

                }
                else
                {
                    flag = false;
                }
            }
            return main;
        }

        private bool FlightSection()
        {
            var flag = true;
            var main = false;
            while (flag)
            {
                var seatClass = SelectSection();
                writer.Write("Airline associate with flight:");
                var airlineName = reader.ReadLine();
                writer.Write("Flight identification number:");
                var id = reader.ReadLine();
                writer.Write("Rows of section:");
                var rows = ParseString("Rows of section:");
                writer.Write("Columns of section:");
                var colms = ParseString("Month of flight:");

                var message = this.manager.CreateSection(airlineName, id, rows, colms, seatClass);
                writer.WriteLine(message);

                if (message.Contains("successfully") == false)
                {
                    writer.Write("Do you want to return in main menu (Y/N): ");
                    flag = ReurnToMainMenu();
                    main = !flag;
                    writer.WriteLine("");
                }
                else
                {
                    flag = false;
                }
            }
            return main;
        }

        private void Book()
        {
            var flag = true;
            while (flag)
            {

                ConsoleSettings.Clear();
                var seatClass = SelectSection();
                writer.Write("Airline associate with flight:");
                var airlineName = reader.ReadLine();
                writer.Write("Flight identification number:");
                var id = reader.ReadLine();
                writer.Write("Rows of section:");
                var row = ParseString("Row of section:");
                writer.Write("Column of section:");
                var colmn = reader.ReadLine();

                var message = this.manager.BookSeat(airlineName, id,seatClass,row,colmn[0]);
                writer.WriteLine(message);

                if (message.Contains("successfully") == false)
                {
                    writer.Write("Do you want to return in main menu (Y/N): ");
                    flag = ReurnToMainMenu();
                    writer.WriteLine("");
                    Run();
                }
                else
                {
                    flag = false;
                }
            }
        }
        private void Details()
        {
            ConsoleSettings.Clear();
            writer.WriteLine(this.manager.DisplaySystemDetails());
        }
        private void HelpMenu()
        {
            ConsoleSettings.Clear();
            writer.WriteLine(Help());

        }

        private int SelectSection()
        {
            ConsoleSettings.Clear();
            writer.WriteLine("Select type of section:");
            var sectionTypes = new List<string>() { " -First class", " -Business class", " -Economy class" };
            sectionTypes.ForEach(x => writer.WriteLine(x));
            var orgPossition = ConsoleSettings.ConsolePosstionRow();
            while (true)
            {
                var key = ConsoleSettings.Key;
                var curentRow = ConsoleSettings.ConsolePosstionRow();

                switch (key.Key)
                {
                    case ConsoleKey.Escape:
                        Run();
                        break;
                    case ConsoleKey.UpArrow:
                        ConsoleSettings.SetPossition(0, curentRow - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        ConsoleSettings.SetPossition(0, curentRow + 1);
                        ConsoleSettings.ResetColor();
                        break;
                    case ConsoleKey.Enter:
                        ConsoleSettings.SetPossition(0, orgPossition + 2);
                        return curentRow;
                    default:
                        break;
                }
            }
        }
        private void SetConsoleSize()
        {
            ConsoleSettings.Height = 30;
            ConsoleSettings.Width = 120;
            ConsoleSettings.SetSize();
        }

        private int ParseString(string text)
        {
            while (true)
            {
                try
                {
                    return int.Parse(reader.ReadLine());
                }
                catch (Exception)
                {

                    Console.WriteLine("Not a valid number");
                    Console.Write(text);
                }
            }
        }

        private bool ReurnToMainMenu() =>
             ConsoleSettings.Key.Key switch
             {
                 ConsoleKey.Y => false,
                 ConsoleKey.N => true,
                 ConsoleKey.Escape => false,
                 _ => true,
             };

    }
}
