using ConsoleAppMarsRover.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.Models
{
    public class GridPlateau
    {
      
        public readonly Point origin;
        public readonly Point limit;
        public List<IRover> rovers { get; set; } = new List<IRover>();
        public GridPlateau( Point origin,Point limit)
        {
            this.origin = origin;
            this.limit = limit;
        }

      
    }
}
