using ConsoleAppMarsRover.Contracts;
using ConsoleAppMarsRover.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.Models
{
    public class Rover : IRover
    {

        public readonly int range;

        private readonly Position position;

        public Rover(Position pos, int r)
        {
            this.position = pos;            
            this.range = r;

        }

        /// <summary>
        /// This functions compare the rover positions with a given position
        /// </summary>
        public bool confirmPosition(Position pos)
        {
            if (this.position.point.comparePoint(pos.point) && (pos.direction == this.position.direction))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void setDirection(CardinalEnum direction)
        {
            this.position.direction = direction;
        }

        /// <summary>
        /// This functions returns the actual position of the Rover
        /// </summary>
        public Position getPositionWithDirection()
        {
            return position;
        }

        /// <summary>
        /// This method turns the rover, using the TurnEnum to determine the turn direction.        
        /// </summary>
        public void Turn(TurnEnum turn)
        {
            switch (this.position.direction)
            {
                case CardinalEnum.N:
                    if (turn == TurnEnum.L) setDirection(CardinalEnum.W);
                    if (turn == TurnEnum.R) setDirection(CardinalEnum.E);
                    break;

                case CardinalEnum.S:
                    if (turn == TurnEnum.L) setDirection(CardinalEnum.E);
                    if (turn == TurnEnum.R) setDirection(CardinalEnum.W);
                    break;
                case CardinalEnum.E:
                    if (turn == TurnEnum.L) setDirection(CardinalEnum.N);
                    if (turn == TurnEnum.R) setDirection(CardinalEnum.S);
                    break;
                case CardinalEnum.W:
                    if (turn == TurnEnum.L) setDirection(CardinalEnum.S);
                    if (turn == TurnEnum.R) setDirection(CardinalEnum.N);
                    break;
            }
        }

        /// <summary>
        /// This method moves the rover, adding the range to x or y values
        /// 
        /// </summary>
        public void Move()
        {
            switch (this.position.direction)
            {
                case CardinalEnum.N:
                    this.position.point.N += this.range;
                    break;
                case CardinalEnum.S:
                    this.position.point.N -= this.range;
                    break;
                case CardinalEnum.E:
                    this.position.point.E += this.range;
                    break;
                case CardinalEnum.W:
                    this.position.point.E -= this.range;
                    break;
            }
        }

    }
}
