namespace ABS_xTest
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;
    using Facade;
    using Models;
    using Moq;

    using static Mocks.MockABS;
    public class SystemManagerTest
    {
        [Theory]
        [InlineData("SVA")]
        [InlineData("PLD")]
        [InlineData("VRN")]
        public void CreateValidAirport(string name)
        {
            //Arrange
            var system = new SystemManager();
            var systemMock = new Mock<ISystemManager>();
            //Act
            //    system.CreateAirport(name);
            //Assert
            systemMock.Verify(x => x.CreateAirline(name)); 
        }


    }
}
