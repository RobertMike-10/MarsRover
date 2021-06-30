using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.Contracts
{
    interface IRoverController
    {
        public IRover rover { get; set; }
        public GridPlateau plateau { get; set; }
        public string GetRoverPosition(); 
        public void ProccessBatch(string batch);
        public ResultMessage confirmPosition(string pos);


    }
}
