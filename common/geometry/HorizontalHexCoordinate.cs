using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Geometry
{
    /// <summary>
    ///  An object representing coordinates in an hex grid
    /// </summary>
    public class HorizontalHexCoordinate
    {
        public static string[] Directions = new string[] { "e", "ne", "se", "w", "nw", "sw" };

        public Int64 X { get; set; }
        
        public Int64 Y { get; set; }

        public HorizontalHexCoordinate()
        {
            X = 0;
            Y = 0;
        }

        /// <summary>
        /// Move the coordinates in a given direction
        /// Direction is given using cardinal points like : e, ne, se, w, sw, nw
        /// </summary>
        /// <param name="direction"></param>
        public void Move(string direction)
        {
            switch (direction)
            {
                case "e":
                {
                    X += 1;
                    break;
                }
                case "ne":
                {
                    Y += 1;
                    break;
                }
                case "se":
                {
                    X += 1;
                    Y -= 1;
                    break;
                }
                case "w":
                {
                    X -= 1;
                    break;
                }
                case "sw":
                {
                    Y -= 1;
                    break;
                }
                case "nw":
                {
                    X -= 1;
                    Y += 1;
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