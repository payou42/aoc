using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common
{
    public class HexCoordinate
    {
        public Int64 X { get; set; }
        
        public Int64 Y { get; set; }

        public HexCoordinate()
        {
            X = 0;
            Y = 0;
        }

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