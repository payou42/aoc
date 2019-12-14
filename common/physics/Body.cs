using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Physics
{  
    public class Body
    {
        public Point3D Position { get; set; }

        public Point3D Velocity { get; set; }

        public Body()
        {          
            Position = new Point3D() { X = 0, Y = 0, Z = 0};  
            Velocity = new Point3D() { X = 0, Y = 0, Z = 0};  
        }

        public double Energy
        {
            get
            {
                return Position.Energy * Velocity.Energy;
            }
        }

        public void Move()
        {
            Position += Velocity;
        }

        public static void ApplyGravity(Body a, Body b)
        {
            if (a.Position.X > b.Position.X)
            {
                a.Velocity.X -= 1;
                b.Velocity.X += 1;
            }
            else if (a.Position.X < b.Position.X)
            {
                a.Velocity.X += 1;
                b.Velocity.X -= 1;
            }

            if (a.Position.Y > b.Position.Y)
            {
                a.Velocity.Y -= 1;
                b.Velocity.Y += 1;
            }
            else if (a.Position.Y < b.Position.Y)
            {
                a.Velocity.Y += 1;
                b.Velocity.Y -= 1;
            }

            if (a.Position.Z > b.Position.Z)
            {
                a.Velocity.Z -= 1;
                b.Velocity.Z += 1;
            }
            else if (a.Position.Z < b.Position.Z)
            {
                a.Velocity.Z += 1;
                b.Velocity.Z -= 1;
            }
        }
    }
}