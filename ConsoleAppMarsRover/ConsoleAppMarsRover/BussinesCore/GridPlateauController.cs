using ConsoleAppMarsRover.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppMarsRover.BussinesCore
{
    public class GridPlateauController
    {
        public GridPlateau plateau;
        public ResultMessage CreateGridPlateau(Point origin, string limit)
        {
            
            var array = limit.Split(' ');
            var result = validLimitFormat(array);
            if (result.result == false)
                return result;
            Point pointLimit = createPoint(array);
            this.plateau = new GridPlateau(origin, pointLimit);
            result.result = true;
            return result;
        }

        private Point createPoint(string[] array)
        {
            return new Point(Int32.Parse(array[0]), Int32.Parse(array[1]));
        }
        private ResultMessage validLimitFormat(string[] array)
        {
            ResultMessage result = new ResultMessage();
            //Wrong length
            if (array.Length < 2)
            {
                result.result = false;
                result.Message = "The position limit entered is in a wrong format. Use: x y";
                return result;
            }

            // First string must be number
            if (!array[0].All(char.IsDigit))
            {
                result.result = false;
                result.Message = "The first digit must be numeric. Represents x value";
                return result;
            }

            //Second string must be number
            if (!array[1].All(char.IsDigit))
            {
                result.result = false;
                result.Message = "The second digit must be numeric. Represents y value";
                return result;
            }
                      
                //plateau must have dimensions. 0 0 is considered wrong
                if ((Int32.Parse(array[0])< 1) && (Int32.Parse(array[1]) < 1))
            {
                result.result = false;
                result.Message = "The plateau must have dimensions. Specify a value grater than 0 in one dimension";
                return result;
            }

            result.result = true;
            return result;
        }
    }
}
