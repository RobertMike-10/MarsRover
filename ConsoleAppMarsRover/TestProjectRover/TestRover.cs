using System;
using Xunit;
using ConsoleAppMarsRover.Models;
using ConsoleAppMarsRover.Enums;

namespace TestProjectRover
{
    public class TestRover
    {
        
        /// <summary>
        /// Test the Rover creation, checking its not null, the range and the position
        /// </summary>
        [Fact]
        public void TestCreationRover()
        {
            int x = 0;
            int y = 0;
            int range = 2;
            Point point = new Point(y, x);
            Position position = new Position { point = point, direction = CardinalEnum.N };
            Rover rover = new Rover(position, range);
            Assert.NotNull(rover);
            Assert.Equal(rover.range, range);
            Assert.Equal(rover.getPositionWithDirection(), position);
        }

        /// <summary>
        /// Test the ConfirmPositionMethod when ths positions area equal
        /// </summary>
        [Fact]
        public void TestConfirmPositionEqual()
        {
            int x = 0;
            int y = 0;
            int range = 2;
            Point point = new Point(y, x);
            Position position = new Position { point = point, direction = CardinalEnum.N };
            Rover rover = new Rover(position, range);
            Assert.True(rover.confirmPosition(position));
        }
        /// <summary>
        /// Test the ConfirmPositionMethod when ths positions area different
        /// First, test with diferent point, second with different direction
        /// </summary>
        [Fact]
        public void TestConfirmPositionDifferent()
        {
            int x = 0;
            int y = 0;
            int range = 2;
            Point point = new Point(y, x);
            Position position = new Position { point = point, direction = CardinalEnum.N };
            Rover rover = new Rover(position, range);

            // change point
            Position position2 = new Position { point = new Point(position.point.N, position.point.E), direction = CardinalEnum.N };
            position2.point.N = 2;
            Assert.False(rover.confirmPosition(position2));

            //Change direction
            position2.point.N = 0;
            position2.direction = CardinalEnum.S;
            Assert.False(rover.confirmPosition(position2));

        }
        /// <summary>
        /// Test the getPositionWithDirection method        
        /// </summary>
        [Fact]
        public void TestGetPositon()
        {

            int x = 0;
            int y = 0;
            int range = 2;
            Point point = new Point(y, x);
            Position position = new Position { point = point, direction = CardinalEnum.N };
            Rover rover = new Rover(position, range);

            Assert.NotNull(rover.getPositionWithDirection());
            Assert.Equal(rover.getPositionWithDirection(), position);
        }
        /// <summary>
        /// Test the Turn method. Init facing Norht, then turn Right , the rover mut be facing East.  
        /// Then turn Rigth to face South. Turn Rigth to face West. Finally turn Left and face the South
        /// </summary>
        [Fact]
        public void TestTurn()
        {
            Rover rover = CreateRover();
            var turn = TurnEnum.R;
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.E, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.S, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.W, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.N, rover.getPositionWithDirection().direction);
            turn = TurnEnum.L;
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.W, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.S, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.E, rover.getPositionWithDirection().direction);
            rover.Turn(turn);
            Assert.Equal(CardinalEnum.N, rover.getPositionWithDirection().direction);
        }

        /// <summary>
        /// Test the Move Metod. Move 1 position
        /// </summary>
        [Fact]
        public void TestMove()
        {
            Rover rover = CreateRover();
            rover.Move();
            Position pos = rover.getPositionWithDirection();
            Assert.Equal(1, pos.point.N);
        }

        private Rover CreateRover()
        {
            int x = 0;
            int y = 0;
            int range = 1;
            Point point = new Point(y, x);
            Position position = new Position { point = point, direction = CardinalEnum.N };
            Rover rover = new Rover(position, range);
            return rover;
        }
    }
}
