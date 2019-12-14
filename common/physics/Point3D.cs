using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Physics
{  
    public class Point3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Energy
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
            }
        }

        public static Point3D operator+(Point3D a, Point3D b)
        {
            return new Point3D() { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
        }
    }
}