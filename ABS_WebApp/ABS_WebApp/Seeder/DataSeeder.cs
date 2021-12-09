using ABS_SystemManager.Interfaces;

namespace ABS_WebApp.Seeder
{
    public static class DataSeeder
    {
        private static bool _seeded;

        public static void Seed(ISystemManager manager)
        {
            if (_seeded)
            {
                return;
            }

            manager.CreateAirport("SFA");
            manager.CreateAirport("PLD");
            manager.CreateAirport("VRN");
            manager.CreateAirport("PRS");

            manager.CreateAirline("BGAir");
            manager.CreateAirline("RUAir");
            manager.CreateAirline("FRAir");

            manager.CreateFlight("BGAir", "SFA", "PLD", 2022, 10, 5, "BG541");
            manager.CreateFlight("BGAir", "PLD", "SFA", 2022, 10, 5, "BG542");
            manager.CreateFlight("BGAir", "SFA", "PRS", 2022, 10, 5, "BG543");
            manager.CreateFlight("BGAir", "VRN", "PRS", 2022, 10, 5, "BG544");

            manager.CreateFlight("RUAir", "SFA", "PLD", 2022, 10, 5, "RU541");
            manager.CreateFlight("RUAir", "PLD", "SFA", 2022, 10, 5, "RU542");
            manager.CreateFlight("RUAir", "SFA", "PRS", 2022, 10, 5, "RU543");
            manager.CreateFlight("RUAir", "VRN", "PRS", 2022, 10, 5, "RU544");
            
            manager.CreateFlight("FRAir", "SFA", "PLD", 2022, 10, 5, "FR541");
            manager.CreateFlight("FRAir", "PLD", "SFA", 2022, 10, 5, "FR542");
            manager.CreateFlight("FRAir", "SFA", "PRS", 2022, 10, 5, "FR543");
            manager.CreateFlight("FRAir", "VRN", "PRS", 2022, 10, 5, "FR544");

            manager.CreateSection("BGAir", "BG541", 5, 5, 1);
            manager.CreateSection("BGAir", "BG541", 5, 5, 2);
            manager.CreateSection("BGAir", "BG541", 5, 5, 3);

            manager.CreateSection("BGAir", "BG542", 5, 5, 1);
            manager.CreateSection("BGAir", "BG542", 5, 5, 2);
            manager.CreateSection("BGAir", "BG542", 5, 5, 3);

            manager.CreateSection("BGAir", "BG543", 5, 5, 1);
            manager.CreateSection("BGAir", "BG543", 5, 5, 2);
            manager.CreateSection("BGAir", "BG543", 5, 5, 3);

            manager.CreateSection("BGAir", "BG544", 5, 5, 1);
            manager.CreateSection("BGAir", "BG544", 5, 5, 2);
            manager.CreateSection("BGAir", "BG544", 5, 5, 3);

            manager.CreateSection("RUAir", "RU541", 5, 5, 1);
            manager.CreateSection("RUAir", "RU541", 5, 5, 2);
            manager.CreateSection("RUAir", "RU541", 5, 5, 3);

            manager.CreateSection("RUAir", "RU542", 5, 5, 2);
            manager.CreateSection("RUAir", "RU542", 5, 5, 1);
            manager.CreateSection("RUAir", "RU542", 5, 5, 3);

            manager.CreateSection("RUAir", "RU543", 5, 5, 2);
            manager.CreateSection("RUAir", "RU543", 5, 5, 1);
            manager.CreateSection("RUAir", "RU543", 5, 5, 3);

            manager.CreateSection("RUAir", "RU544", 5, 5, 2);
            manager.CreateSection("RUAir", "RU544", 5, 5, 1);
            manager.CreateSection("RUAir", "RU544", 5, 5, 3);

            _seeded = true;
        }
    }
}
