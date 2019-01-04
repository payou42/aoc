using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Containers;
namespace Aoc
{
    public class Day201823 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Box
        {
            public int X1 { get; set; }

            public int X2 { get; set; }

            public int Y1 { get; set; }

            public int Y2 { get; set; }

            public int Z1 { get; set; }

            public int Z2 { get; set; }

            public (int, int, int) ClosestToOrigin()
            {
                int x = 0;
                if (X1 > 0)
                {
                    x = X1;
                }
                if (X2 < 0)
                {
                    x = X2;
                }

                int y = 0;
                if (Y1 > 0)
                {
                    y = Y1;
                }
                if (Y2 < 0)
                {
                    y = Y2;
                }

                int z = 0;
                if (Z1 > 0)
                {
                    z = Z1;
                }
                if (Z2 < 0)
                {
                    z = Z2;
                }

                return (x, y, z);
            }

            public int DistanceToOrigin()
            {
                var (x, y, z) = ClosestToOrigin();
                return Math.Abs(x) + Math.Abs(y) + Math.Abs(z);
            }

            public Box()
            {                
            }

            public Box(int bound)
            {
                X1 = -bound;
                X2 =  bound;
                Y1 = -bound;
                Y2 =  bound;
                Z1 = -bound;
                Z2 =  bound;
            }

            public Box[] Split()
            {
                Box[] boxes = new Box[8]
                {
                    new Box { X1 = X1, Y1 = Y1, Z1 = Z1, X2 = (X1 + X2) / 2, Y2 = (Y1 + Y2) / 2, Z2 = (Z1 + Z2) / 2 },
                    new Box { X1 = 1 + (X1 + X2) / 2, Y1 = Y1, Z1 = Z1, X2 = X2, Y2 = (Y1 + Y2) / 2, Z2 = (Z1 + Z2) / 2 },
                    new Box { X1 = X1, Y1 = 1 + (Y1 + Y2) / 2, Z1 = Z1, X2 = (X1 + X2) / 2, Y2 = Y2, Z2 = (Z1 + Z2) / 2 },
                    new Box { X1 = 1 + (X1 + X2) / 2, Y1 = 1 + (Y1 + Y2) / 2, Z1 = Z1, X2 = X2, Y2 = Y2, Z2 = (Z1 + Z2) / 2 },
                    new Box { X1 = X1, Y1 = Y1, Z1 = 1 + (Z1 + Z2) / 2, X2 = (X1 + X2) / 2, Y2 = (Y1 + Y2) / 2, Z2 = Z2 },
                    new Box { X1 = 1 + (X1 + X2) / 2, Y1 = Y1, Z1 = 1 + (Z1 + Z2) / 2, X2 = X2, Y2 = (Y1 + Y2) / 2, Z2 = Z2 },
                    new Box { X1 = X1, Y1 = 1 + (Y1 + Y2) / 2, Z1 = 1 + (Z1 + Z2) / 2, X2 = (X1 + X2) / 2, Y2 = Y2, Z2 = Z2 },
                    new Box { X1 = 1 + (X1 + X2) / 2, Y1 = 1 + (Y1 + Y2) / 2, Z1 = 1 + (Z1 + Z2) / 2, X2 = X2, Y2 = Y2, Z2 = Z2 }
                };

                return boxes;
            }
        }

        public class Nanobot
        {
            public int X { get; set; }

            public int Y;

            public int Z;

            public int Radius;

            public bool IntersectWithBox(Box box)
            {
                // Take the distance from the point of the box closest to the center of the nanobot
                int d = 0;
                if (box.X1 > X)
                {
                    d += Math.Abs(box.X1 - X);
                }
                if (box.X2 < X)
                {
                    d += Math.Abs(box.X2 - X);
                }
                if (box.Y1 > Y)
                {
                    d += Math.Abs(box.Y1 - Y);
                }
                if (box.Y2 < Y)
                {
                    d += Math.Abs(box.Y2 - Y);
                }
                if (box.Z1 > Z)
                {
                    d += Math.Abs(box.Z1 - Z);
                }
                if (box.Z2 < Z)
                {
                    d += Math.Abs(box.Z2 - Z);
                }

                return (d <= Radius);
            }

            public static int Distance(Nanobot a, Nanobot b)
            {
                return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z);
            }
        }

        private Nanobot[] _nanobots;

        public Day201823()
        {
            Codename = "2018-23";
            Name = "Experimental Emergency Teleportation";
        }

        public void Init()
        {
            // Build the list of nanobot
            _nanobots = Aoc.Framework.Input.GetStringVector(this).Select(l => ParseNanobot(l)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Get the strongest nanobot
                Nanobot strongest = _nanobots.OrderByDescending(n => n.Radius).First();

                // Count the nanobot in range
                return _nanobots.Count(n => Nanobot.Distance(n, strongest) <= strongest.Radius).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Find the coordinates with the most nanobots in range
                int minSize = _nanobots.Min(n => Math.Min(Math.Min(n.X, n.Y), n.Z) - n.Radius);
                int maxSize = _nanobots.Min(n => Math.Max(Math.Max(n.X, n.Y), n.Z) + n.Radius);
                int maxBound = Math.Max(Math.Abs(minSize), Math.Abs(maxSize));
                int boxSize = 1;
                while (boxSize < maxBound)
                {
                    boxSize *= 2;
                }

                // Use a priority queue to process cubes in order
                // Priority is given by :
                // 1. Number of bots in range
                // 2. Distance to the origin
                // Hence the priority weight given by ((long)nbBot << 32) + (long)dst)
                PriorityQueue<Box> queue = new PriorityQueue<Box>();
                queue.Enqueue(new Box(boxSize), 0);

                while (queue.TryDequeueMax(out var box))
                {
                    // If it's the minimal size
                    if (box.X2 == box.X1)
                    {
                        // This is the closest
                        return box.DistanceToOrigin().ToString();
                    }

                    // Split the box in 8 boxes
                    Box[] split = box.Split();
                    foreach (Box b in split)
                    {
                        int nbBot = _nanobots.Count(n => n.IntersectWithBox(b));
                        int dst = box.DistanceToOrigin();
                        queue.Enqueue(b, ((long)nbBot << 32) + (long)dst);
                    }
                }
            }

            return "";
        }

        private Nanobot ParseNanobot(string line)
        {
            // Parse a line like
            // pos=<76140848,-3604484,65709148>, r=91714805
            // into a Nanobot instance
            string[] items = line.Split('=', '>', '<', ',');

            // Items should be
            // 0   1 2        3        4        5 6 7  
            // pos ¤ 76140848 -3604484 65709148 ¤ r 91714805
            return new Nanobot
            {
                X = int.Parse(items[2].Trim()),
                Y = int.Parse(items[3].Trim()),
                Z = int.Parse(items[4].Trim()),
                Radius = int.Parse(items[7].Trim())
            };
        }
    }   
}