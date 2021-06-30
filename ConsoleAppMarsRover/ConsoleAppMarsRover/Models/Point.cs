using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.Models
{
    public class Point
    {
        public int N { get; set; }
        public int E { get; set; }

        public Point (int n, int e)
        {
            this.N = n;
            this.E = e;
        }

        public bool comparePoint (Point p)
        {
           if ((this.N == p.N) && (this.E==p.E) )
           {
                return true;
            }
           else
            {
                return false;
            }

        }
    }
}
