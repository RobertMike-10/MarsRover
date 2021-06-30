using System;
using Xunit;
using ConsoleAppMarsRover.Models;
using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.BussinesCore;
using ConsoleAppMarsRover.Contracts;

namespace TestProjectRover
{
    public class TestPlateauController
    {
        /// <summary>
        /// Test the Plateu creation. Right format
        /// </summary>
        [Fact]
        public void CreateGridPlateauTrue()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            string limit = "8 8";
            var controller = new GridPlateauController();
            var result = controller.CreateGridPlateau(originPoint, limit);

            Assert.True(result.result);
            Assert.NotNull(controller.plateau);
            
        }

        /// <summary>
        /// Test the Plateu creation. Wrong Format
        /// </summary>
        [Fact]
        public void CreateGridPlateauFalse()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            string limit = "8";
            var controller = new GridPlateauController();
            var result = controller.CreateGridPlateau(originPoint, limit);

            Assert.False(result.result);
            Assert.Equal("The position limit entered is in a wrong format. Use: x y",result.Message);

            limit = "X 1";
             result = controller.CreateGridPlateau(originPoint, limit);
            Assert.False(result.result);
            Assert.Equal("The first digit must be numeric. Represents x value", result.Message);

            limit = "1 X";
            result = controller.CreateGridPlateau(originPoint, limit);
            Assert.False(result.result);
            Assert.Equal("The second digit must be numeric. Represents y value", result.Message);

            limit = "0 0";
            result = controller.CreateGridPlateau(originPoint, limit);
            Assert.False(result.result);
            Assert.Equal("The plateau must have dimensions. Specify a value grater than 0 in one dimension", result.Message);

            }
    }
}
