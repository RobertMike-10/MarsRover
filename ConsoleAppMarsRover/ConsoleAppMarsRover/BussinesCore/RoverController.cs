using ConsoleAppMarsRover.Contracts;
using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.BussinesCore
{
    public class RoverController : IRoverController
    {
        public IRover rover { get; set; }
        public GridPlateau plateau { get; set; }        

        public RoverController(IRover rover, GridPlateau plateau)
        {
            this.plateau = plateau;
            this.rover = rover;
        }

        /// <summary>
        /// This method allows to create a Rover    
        /// </summary>
        public static IRover CreateRover(Point initPoint, CardinalEnum initDirection, int range)
        {
            IRover rover = new Rover(new Position {point = new Point(initPoint.N,initPoint.E) ,direction= initDirection }, range);
            return rover;
        }

        /// <summary>
        /// This method allows to turn rover. Using the command String    
        /// </summary>
        private void TurnRover(string direction)
        {            
            switch (direction)
            {
                case nameof(TurnEnum.L) :
                    this.rover.Turn(TurnEnum.L);
                    break;                    
                case nameof(TurnEnum.R):
                    this.rover.Turn(TurnEnum.R);
                    break;
             }
        }


        /// <summary>
        /// Returns the actual position in string format x y z  
        /// </summary>
        public string GetRoverPosition()
        {
            Position actualPos = this.rover.getPositionWithDirection();
            return Convert.ToString(actualPos.point.E) + " " + Convert.ToString(actualPos.point.N) + " " + GetDirectionString(actualPos.direction);
        }

        /// <summary>
        /// Returns the direction in string, translating from the enum
        /// </summary>
        private string GetDirectionString(CardinalEnum cardinal)
        {
          switch (cardinal)
            {
                case CardinalEnum.N:
                    return nameof(CardinalEnum.N);                 
                case CardinalEnum.S:
                    return nameof(CardinalEnum.S);
                case CardinalEnum.E:
                    return nameof(CardinalEnum.E);
                case CardinalEnum.W:
                    return nameof(CardinalEnum.W);
            }
            return "";
        }

        /// <summary>
        /// Moves the Rover
        /// </summary>
        private void MoveRover()
        {
            
            Position actualPos = this.rover.getPositionWithDirection();
            //check limits in plateau, to avoid the loss of the rover 
            if (CheckLimit(actualPos) == false)
                return;
            //check collisions with the previous rovers, to avoid the collision
            if (CheckRoverCollision(actualPos))
                return;
            this.rover.Move();
        }

        /// <summary>
        /// Checks limit of plateau to avoid osing the rover
        /// </summary>
        private bool CheckLimit(Position actualPos)
        {
            //moving North but we are in the limit
            if ((actualPos.direction == CardinalEnum.N) && (actualPos.point.N == this.plateau.limit.N))
            {
                return false;
            }
            //moving South but we are in the origin
            if ((actualPos.direction == CardinalEnum.S) && (actualPos.point.N == this.plateau.origin.N))
            {
                return false;
            }
            //moving East but we are in the limit
            if ((actualPos.direction == CardinalEnum.E) && (actualPos.point.E == this.plateau.limit.E))
            {
                return false;
            }
            //moving West but we are in the origin
            if ((actualPos.direction == CardinalEnum.W) && (actualPos.point.E == this.plateau.origin.E))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks the position of all the previuos rovers sended to plateau, to avoid collisions
        /// </summary>        
        private bool CheckRoverCollision(Position actualPos)
        {

            //calculate new Position
            Position newPosition = new Position {point = new Point (actualPos.point.N,actualPos.point.E), direction= actualPos.direction };
            
            newPosition = CalculateNewPosition(newPosition);


            foreach (IRover rover in plateau.rovers)
            {
                Position roverPos = rover.getPositionWithDirection();
                if ((newPosition.point.N == roverPos.point.N ) && (newPosition.point.E == roverPos.point.E))
                {
                    return true;
                }
            }

            return false;
        }

        private Position CalculateNewPosition(Position pos)
        {
            Rover rover = this.rover as Rover;
            switch (pos.direction)
            {
                case CardinalEnum.N:
                    pos.point.N += rover.range;
                    return pos;
                case CardinalEnum.S:
                    pos.point.N -= rover.range;
                    return pos;
                case CardinalEnum.E:
                    pos.point.E += rover.range;
                    return pos;
                case CardinalEnum.W:
                    pos.point.E -= rover.range;
                    return pos;
            }
            return null;
        }

        /// <summary>
        /// This method proccess the command sended 
        /// </summary>
        private void ProccesComand(string command)
        {
              switch(command)
            {
                case nameof(TurnEnum.L):
                    TurnRover(command);
                    break;
                case nameof(TurnEnum.R):
                     TurnRover(command);
                    break;
                case "M":
                    MoveRover();
                    break;                

            }
        }

        /// <summary>
        /// This method proccess the batch of commands received in a string format.
        /// </summary>
        public void ProccessBatch(string batch)
        {
            if (batch.Length < 1)
                return;

            for (int i = 0; i < batch.Length; i++)
                ProccesComand(batch.Substring(i, 1));
           
        }

        /// <summary>
        /// This method confirms the position, compares the real rover position, with the string recived
        /// </summary>
        public ResultMessage confirmPosition(string pos)
        {
            
            var array = pos.Split(' ');

            var result = validFormatPosition(array);
            if (result.result == false)
                return result;

            Position actualPos = convertStringPosition(array);
            if (this.rover.confirmPosition(actualPos)== false)
            {
                result.result = false;
                result.Message = "The position is wrong.";
                return result;
            }

            result.result = true;
            return result;
        }

        /// <summary>
        /// This funtion validates the format of string to represents a valid position
        /// Returns ResultMessage, with false if the format is wrong and the message error.
        /// </summary>
        private ResultMessage validFormatPosition(string[] array)
        {
            ResultMessage result = new ResultMessage();
            if (array.Length < 3)
            {
                result.result = false;
                result.Message = "The position entered is in a wrong format. Use: x y direction";
                return result;
            }

            if (!array[0].All(char.IsDigit))
            {
                result.result = false;
                result.Message = "The first digit must be numeric. Represents x value";
                return result;
            }

            if (!array[1].All(char.IsDigit))
            {
                result.result = false;
                result.Message = "The second digit must be numeric. Represents y value";
                return result;
            }

            if (validDirection(array[2]) == false)
            {
                result.result = false;
                result.Message = "The third digit must be a letter that represents the direction of the cardinal point (N, E, W, S)";
                return result;
            }
            result.result = true;
            return result;
        }

        /// <summary>
        /// This funtion converts an array of string, in a valid Position
        /// </summary>
        private Position convertStringPosition(string[] array)
        {
            Position position = new Position() {point = new Point(Int32.Parse(array[1]), Int32.Parse(array[0])),
                direction = getDirectionFromString(array[2])
            };
            
            return position;
        }

        /// <summary>
        /// This funtion converts a string contaning direction, in the equivalent Enum member
        /// </summary>
        private CardinalEnum getDirectionFromString(string direction)
        {
            switch (direction)
            {
                case nameof(CardinalEnum.N) :
                    return CardinalEnum.N;
                case nameof(CardinalEnum.S):
                    return CardinalEnum.S;
                case nameof(CardinalEnum.E):
                    return CardinalEnum.E;
                case nameof(CardinalEnum.W) :
                    return CardinalEnum.W;
            }
            return 0;
        }
        /// <summary>
        /// This funtion validates the string containing the direction. If the string if not contained in CardinalEnum returns false
        /// </summary>        
        private bool validDirection(string direction)
        {
            switch(direction)
            {
                case nameof(CardinalEnum.N):
                case nameof(CardinalEnum.S):
                case nameof(CardinalEnum.E):
                case nameof(CardinalEnum.W):
                        return true;
                default:
                        return false;
            }
        }
    }
}
