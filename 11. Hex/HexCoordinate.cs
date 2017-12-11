using System;
using System.Text;
using System.Collections.Generic;

namespace _11._Hex
{
    class HexCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

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

        public int GetDistance()
        {
            int z = Math.Min(X, Y);
            if (z >= 0)
            {
                Console.WriteLine("z is {0}", z);
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