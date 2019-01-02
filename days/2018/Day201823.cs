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
                // TODO
                return (0, 0, 0);
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

                // Set up heap to work on things first by number of bots in range of box,
                // then by size of box, then by distance to origin
                // The idea is that we first work on a box with the most bots in range.
                // In the event of a tie, work on the larger box.
                // In the event of a tie, work on the one with a min corner closest
                // to the origin.
                PriorityQueue<Box> queue = new PriorityQueue<Box>();

                while (queue.TryDequeueMax(out var box))
                {
                    if (box.X2 - box.X1 == 1)
                    {
                        // This is the closest
                        var (x, y, z) = box.ClosestToOrigin();
                        return $"{x},{y},{z}";
                    }
                }

                /*
                workheap = [(-len(bots), -2*boxsize, 3*boxsize, initial_box)]
                while workheap:
                    (negreach, negsz, dist_to_orig, box) = heapq.heappop(workheap)
                    if negsz == -1:
                        print("Found closest at %s dist %s (%s bots in range)" %
                            (str(box[0]), dist_to_orig, -negreach))
                        break
                    newsz = negsz // -2
                    for octant in [(0, 0, 0), (0, 0, 1), (0, 1, 0), (0, 1, 1),
                                (1, 0, 0), (1, 0, 1), (1, 1, 0), (1, 1, 1)]:
                        newbox0 = tuple(box[0][i] + newsz * octant[i] for i in (0, 1, 2))
                        newbox1 = tuple(newbox0[i] + newsz for i in (0, 1, 2))
                        newbox = (newbox0, newbox1)
                        newreach = intersect_count(newbox)
                        heapq.heappush(workheap,
                                    (-newreach, -newsz, d3(newbox0, (0, 0, 0)), newbox))
                */
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