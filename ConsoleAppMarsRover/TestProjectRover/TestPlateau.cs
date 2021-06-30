using System;
using Xunit;
using ConsoleAppMarsRover.Models;
using ConsoleAppMarsRover.Enums;

namespace TestProjectRover
{

    public class TestPlateau
    {
        /// <summary>
        /// Test the Rover creation, checking its not null, the range and the position
        /// </summary>
        [Fact]
        public void TestPlateauCreation()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            int x1 = 8;
            int y1 = 8;
            Point finalPoint = new Point(y1, x1);
            GridPlateau plateau = new GridPlateau(originPoint, finalPoint);
            Assert.NotNull(plateau);
            Assert.Equal(originPoint, plateau.origin);
            Assert.Equal(finalPoint, plateau.limit);
        }

    }
}
