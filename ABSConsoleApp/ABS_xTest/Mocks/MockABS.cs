namespace ABS_xTest.Mocks
{
    using Moq;
    using Models.Contracts;
    using Facade;

    public static class MockABS
    {
        public static IAirport AirportMock(string name = null)
        {
            var mockAirport = new Mock<IAirport>();
            mockAirport.SetupGet(x => x.Name).Returns(name);
            return mockAirport.Object;

        }

        public static IAirline AirlineMock(string name = null)
        {
            var mockAirport = new Mock<IAirline>();
            mockAirport.SetupGet(x => x.Name).Returns(name);
            return mockAirport.Object;

        }

        public static IFlight FlightMock(string id = null)
        {
            var mockFlight = new Mock<IFlight>();
            mockFlight.SetupGet(x => x.Id).Returns(id);
            return mockFlight.Object;
        }

        public static IFlightSection FlightSectionMock()
        {
            var mockFlight = new Mock<IFlightSection>();
            return mockFlight.Object;
        }

        public static ISeat SeatMock()
        {
            var mockFlight = new Mock<ISeat>();
            return mockFlight.Object;
        }

        public static ISystemManager SystemManagerMock()
        {
            var mockManager = new Mock<ISystemManager>();
            mockManager.Setup(x => x.CreateAirline("BGAir"));
            return mockManager.Object;
        }
    }
}
