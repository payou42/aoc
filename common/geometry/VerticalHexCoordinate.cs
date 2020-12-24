using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Geometry
{
    /// <summary>
    ///  An object representing coordinates in an hex grid
    /// </summary>
    public class VerticalHexCoordinate
    {
        public static string[] Directions = new string[] { "n", "ne", "se", "s", "nw", "sw" };

        public Int64 X { get; set; }
        
        public Int64 Y { get; set; }

        public VerticalHexCoordinate()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Move the coordinates in a given direction
        /// Direction is given using cardinal points like : n, ne, se, s, sw, nw
        /// </summary>
        /// <param name="direction"></param>
        public void Move(string direction)
        {
            switch (direction)
            {
                case "n":
                {
                    Y += 1;
                    break;
                }
                case "ne":
                {
                    X += 1;
                    Y += 1;
                    break;
                }
                case "se":
                {
                    X += 1;
                    break;
                }
                case "s":
                {
                    Y -= 1;
                    break;
                }
                case "sw":
                {
                    X -= 1;
                    Y -= 1;
                    break;
                }
                case "nw":
                {
                    X -= 1;
                    break;
                }                
            }
        }

        /// <summary>
        /// Get the minimum number of moves between the coordinates and the center (0, 0)
        /// </summary>
        /// <returns>The distance from the center</returns>
        public Int64 GetDistance()
        {
            Int64 z = Math.Min(X, Y);
            if (z >= 0)
            {
                return (X + Y - z);                
            }
            z = Math.Min(-X, -Y);
            if (z >= 0)
            {
                return (-X - Y - z);
            }
            return Math.Abs(X) + Math.Abs(Y);            
        }
    }
}