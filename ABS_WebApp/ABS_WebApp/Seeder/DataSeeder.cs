using ABS_SystemManager.Interfaces;

namespace ABS_WebApp.Seeder
{
    public class DataSeeder
    {
        private ISystemManager _manager;
        public DataSeeder(ISystemManager manager)
        {
            _manager = manager;
        }

        public void Seed()
        {
            _manager.CreateAirport("SFA");
            _manager.CreateAirport("PLD");
            _manager.CreateAirport("VRN");
            _manager.CreateAirport("PRS");

            _manager.CreateAirline("BGAir");
            _manager.CreateAirline("RUAir");
            _manager.CreateAirline("FRAir");

            _manager.CreateFlight("BGAir", "SFA", "PLD", 2022, 10, 5, "BG541");
            _manager.CreateFlight("BGAir", "PLD", "SFA", 2022, 10, 5, "BG542");
            _manager.CreateFlight("BGAir", "SFA", "PRS", 2022, 10, 5, "BG543");
            _manager.CreateFlight("BGAir", "VRN", "PRS", 2022, 10, 5, "BG544");

            _manager.CreateFlight("RUAir", "SFA", "PLD", 2022, 10, 5, "RU541");
            _manager.CreateFlight("RUAir", "PLD", "SFA", 2022, 10, 5, "RU542");
            _manager.CreateFlight("RUAir", "SFA", "PRS", 2022, 10, 5, "RU543");
            _manager.CreateFlight("RUAir", "VRN", "PRS", 2022, 10, 5, "RU544");
            
            _manager.CreateFlight("FRAir", "SFA", "PLD", 2022, 10, 5, "FR541");
            _manager.CreateFlight("FRAir", "PLD", "SFA", 2022, 10, 5, "FR542");
            _manager.CreateFlight("FRAir", "SFA", "PRS", 2022, 10, 5, "FR543");
            _manager.CreateFlight("FRAir", "VRN", "PRS", 2022, 10, 5, "FR544");

            _manager.CreateSection("BGAir", "ru541", 5, 5, 1);
            _manager.CreateSection("BGAir", "BG541", 5, 5, 2);
            _manager.CreateSection("BGAir", "BG541", 5, 5, 3);

            _manager.CreateSection("BGAir", "BG542", 5, 5, 2);
            _manager.CreateSection("BGAir", "BG542", 5, 5, 1);
            _manager.CreateSection("BGAir", "BG542", 5, 5, 3);

            _manager.CreateSection("BGAir", "BG543", 5, 5, 2);
            _manager.CreateSection("BGAir", "BG543", 5, 5, 1);
            _manager.CreateSection("BGAir", "BG543", 5, 5, 3);

            _manager.CreateSection("BGAir", "BG544", 5, 5, 2);
            _manager.CreateSection("BGAir", "BG544", 5, 5, 1);
            _manager.CreateSection("BGAir", "BG544", 5, 5, 3);

            _manager.CreateSection("RUAir", "RU542", 5, 5, 1);
            _manager.CreateSection("RUAir", "RU541", 5, 5, 2);
            _manager.CreateSection("RUAir", "RU541", 5, 5, 3);

            _manager.CreateSection("RUAir", "RU542", 5, 5, 2);
            _manager.CreateSection("RUAir", "RU542", 5, 5, 1);
            _manager.CreateSection("RUAir", "RU542", 5, 5, 3);

            _manager.CreateSection("RUAir", "RU543", 5, 5, 2);
            _manager.CreateSection("RUAir", "RU543", 5, 5, 1);
            _manager.CreateSection("RUAir", "RU543", 5, 5, 3);

            _manager.CreateSection("RUAir", "RU544", 5, 5, 2);
            _manager.CreateSection("RUAir", "RU544", 5, 5, 1);
            _manager.CreateSection("RUAir", "RU544", 5, 5, 3);

        }
    }
}
