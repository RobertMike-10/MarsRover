using System;
using Xunit;
using ConsoleAppMarsRover.Models;
using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.BussinesCore;
using ConsoleAppMarsRover.Contracts;

namespace TestProjectRover
{
    public class TestRoverController
    {
        /// <summary>
        /// Test the Rover creation, checking its not null, the range and the position
        /// </summary>
        [Fact]
        public void TestCreationRover()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            IRover rover = RoverController.CreateRover(originPoint, CardinalEnum.N, 1);
            Assert.NotNull(rover);
            Assert.True(originPoint.comparePoint(rover.getPositionWithDirection().point));
            Assert.Equal(CardinalEnum.N, rover.getPositionWithDirection().direction);

        }

        private IRover CreateRover()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            IRover rover = RoverController.CreateRover(originPoint, CardinalEnum.N, 1);
            return rover;
        }

        private GridPlateau CreatePlateu()
        {
            int x = 0;
            int y = 0;
            Point originPoint = new Point(y, x);
            int x1 = 8;
            int y1 = 8;
            Point finalPoint = new Point(y1, x1);
            GridPlateau plateau = new GridPlateau(originPoint, finalPoint);
            return plateau;
        }

        private RoverController CreateController()
        {
            var rover = CreateRover();
            var plateau = CreatePlateu();
            var controller = new RoverController(rover, plateau);
            return controller;
        }

        [Fact]
        public void TestCreationController()
        {
            var rover = CreateRover();
            var plateau = CreatePlateu();
            var controller = new RoverController(rover, plateau);
            Assert.NotNull(controller);
            Assert.Equal(rover, controller.rover);
            Assert.Equal(plateau, controller.plateau);

        }

        /// <summary>
        /// Test ConfirmPosition, with wrong position
        /// Case 1: The position is in wrong format
        /// Case 2: Letter in first coordinate
        /// Case 3: Letter in second coordinate
        /// Case 4: Third value is not N, S, E or W
        /// Case 5: Wrong position
        /// </summary>
        [Fact]
        public void TestConfirmPositionTrue()
        {

            var controller = CreateController();

            string position = "0 0 N";
            var result = controller.confirmPosition(position);
            Assert.True(result.result);
        }

        /// <summary>
        /// Test ConfirmPosition, with rigth position
        /// </summary>
        [Fact]
        public void TestConfirmPositionFalse()
        {
            var controller = CreateController();

            string position = "0";
            var result = controller.confirmPosition(position);
            Assert.False(result.result);
            Assert.Equal("The position entered is in a wrong format. Use: x y direction", result.Message);

            position = "E 0 E";
            result = controller.confirmPosition(position);
            Assert.False(result.result);
            Assert.Equal("The first digit must be numeric. Represents x value", result.Message);

            position = "1 X E";
            result = controller.confirmPosition(position);
            Assert.False(result.result);
            Assert.Equal("The second digit must be numeric. Represents y value", result.Message);

            position = "1 2 G";
            result = controller.confirmPosition(position);
            Assert.False(result.result);
            Assert.Equal("The third digit must be a letter that represents the direction of the cardinal point (N, E, W, S)", result.Message);

            position = "1 0 N";
            result = controller.confirmPosition(position);
            Assert.False(result.result);
            Assert.Equal("The position is wrong.", result.Message);

        }


        /// <summary>
        /// Test GetPosition
        /// </summary>
        [Fact]
        public void TestGetPosition()
        {
            var controller = CreateController();
            string position = controller.GetRoverPosition();
            Assert.Equal("0 0 N", position);
        }

        
        /// <summary>
        /// Test Move
        /// </summary>
        [Fact]
        public void ProccessBatchMove()
        {
            var controller = CreateController();
            string command = "MMRML";
            controller.ProccessBatch(command);
            Assert.Equal("1 2 N", controller.GetRoverPosition());
            command = "LMLMLMLMM";
            controller.ProccessBatch(command);
            Assert.Equal("1 3 N", controller.GetRoverPosition());
        }
    }
}
