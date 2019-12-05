using System;
using System.Drawing;

namespace Aoc.Common.Grid
{
    /// <summary>
    /// An horizontal line
    /// </summary>
    public class Segment
    {
        public Segment()
        {
            X1 = 0;
            X2 = 0;
            Y1 = 0;
            Y2 = 0;
            UserData = null;
        }

        public int Y1 { get; set; }

        public int Y2 { get; set; }

        public int X1 { get; set; }

        public int X2 { get; set; }

        public object UserData { get; set; }

        public bool PerpendicularIntersects(Segment other)
        {
            if (this.X1 == this.X2)
            {
                return other.X1 <= this.X1 && other.X2 >= this.X1 && this.Y1 <= other.Y1 && this.Y2 >= other.Y1;
            }
            else
            {
                return other.Y1 <= this.Y1 && other.Y2 >= this.Y1 && this.X1 <= other.X1 && this.X2 >= other.X1;
            }
        }

        public Point IntersectionPoint(Segment other)
        {
            if (this.X1 == this.X2)
            {
                return new Point(this.X1, other.Y1);
            }
            else
            {
                return new Point(other.X1, this.Y1);
            }
        }
    }
}
