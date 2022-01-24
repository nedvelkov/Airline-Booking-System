using ABS_SystemManager.Interfaces;
using System.Threading.Tasks;

namespace ABS_WebApp.Seeder
{
    public class DataSeeder
    {

        public async Task<bool> Seed(ISystemManager manager)
        {
            var test = manager.DisplaySystemDetails();
            var flag = await manager.HasAirport("SFA");
            if (flag)
            {
                return false;
            }
            await manager.CreateAirport("SFA");
            await manager.CreateAirport("PLD");
            await manager.CreateAirport("VRN");
            await manager.CreateAirport("PRS");

            await manager.CreateAirline("BGAir");
            await manager.CreateAirline("RUAir");
            await manager.CreateAirline("FRAir");

            await manager.CreateFlight("BGAir", "SFA", "PLD", 2022, 10, 5, "BG541");
            await manager.CreateFlight("BGAir", "PLD", "SFA", 2022, 10, 5, "BG542");
            await manager.CreateFlight("BGAir", "SFA", "PRS", 2022, 10, 5, "BG543");
            await manager.CreateFlight("BGAir", "VRN", "PRS", 2022, 10, 5, "BG544");

            await manager.CreateFlight("RUAir", "SFA", "PLD", 2022, 10, 5, "RU541");
            await manager.CreateFlight("RUAir", "PLD", "SFA", 2022, 10, 5, "RU542");
            await manager.CreateFlight("RUAir", "SFA", "PRS", 2022, 10, 5, "RU543");
            await manager.CreateFlight("RUAir", "VRN", "PRS", 2022, 10, 5, "RU544");

            await manager.CreateFlight("FRAir", "SFA", "PLD", 2022, 10, 5, "FR541");
            await manager.CreateFlight("FRAir", "PLD", "SFA", 2022, 10, 5, "FR542");
            await manager.CreateFlight("FRAir", "SFA", "PRS", 2022, 10, 5, "FR543");
            await manager.CreateFlight("FRAir", "VRN", "PRS", 2022, 10, 5, "FR544");

            await manager.CreateSection("BGAir", "BG541", 5, 5, 1);
            await manager.CreateSection("BGAir", "BG541", 5, 5, 2);
            await manager.CreateSection("BGAir", "BG541", 5, 5, 3);

            await manager.CreateSection("BGAir", "BG542", 5, 5, 1);
            await manager.CreateSection("BGAir", "BG542", 5, 5, 2);
            await manager.CreateSection("BGAir", "BG542", 5, 5, 3);

            await manager.CreateSection("BGAir", "BG543", 5, 5, 1);
            await manager.CreateSection("BGAir", "BG543", 5, 5, 2);
            await manager.CreateSection("BGAir", "BG543", 5, 5, 3);

            await manager.CreateSection("BGAir", "BG544", 5, 5, 1);
            await manager.CreateSection("BGAir", "BG544", 5, 5, 2);
            await manager.CreateSection("BGAir", "BG544", 5, 5, 3);

            await manager.CreateSection("RUAir", "RU541", 5, 5, 1);
            await manager.CreateSection("RUAir", "RU541", 5, 5, 2);
            await manager.CreateSection("RUAir", "RU541", 5, 5, 3);

            await manager.CreateSection("RUAir", "RU542", 5, 5, 2);
            await manager.CreateSection("RUAir", "RU542", 5, 5, 1);
            await manager.CreateSection("RUAir", "RU542", 5, 5, 3);

            await manager.CreateSection("RUAir", "RU543", 5, 5, 2);
            await manager.CreateSection("RUAir", "RU543", 5, 5, 1);
            await manager.CreateSection("RUAir", "RU543", 5, 5, 3);

            await manager.CreateSection("RUAir", "RU544", 5, 5, 2);
            await manager.CreateSection("RUAir", "RU544", 5, 5, 1);
            await manager.CreateSection("RUAir", "RU544", 5, 5, 3);

            return true;
        }
    }
}
