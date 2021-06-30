using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.Contracts
{
    public interface IRover
    {
        
        public bool confirmPosition(Position pos);
        public void Turn(TurnEnum turn);
        public void Move();
        public Position getPositionWithDirection();
      
    }
}
