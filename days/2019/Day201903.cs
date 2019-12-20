using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201903 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private List<Segment> _horizontals1;

        private List<Segment> _verticals1;

        private List<Segment> _horizontals2;

        private List<Segment> _verticals2;

        private List<(Point, Segment, Segment)> _intersections;

        public Day201903()
        {
            Codename = "2019-03";
            Name = "Crossed Wires";
        }

        public void Init()
        {
            // Get the raw input
            string[][] moves = Aoc.Framework.Input.GetStringMatrix(this, ",");
            _horizontals1 = new List<Segment>();
            _horizontals2 = new List<Segment>();
            _verticals1 = new List<Segment>();
            _verticals2 = new List<Segment>();

            // Extract a list of segments from the input
            ExtractSegments(moves[0], _horizontals1, _verticals1);
            ExtractSegments(moves[1], _horizontals2, _verticals2);

            // Find the intersections
            _intersections = new List<(Point, Segment, Segment)>();
            foreach (Segment h in _horizontals1)
            {
                _intersections.AddRange(_verticals2.Where(v => h.PerpendicularIntersects(v)).Select(v => (h.IntersectionPoint(v), h, v)));
            }

            foreach (Segment h in _horizontals2)
            {
                _intersections.AddRange(_verticals1.Where(v => h.PerpendicularIntersects(v)).Select(v => (h.IntersectionPoint(v), h, v)));
            }
        }

        private void ExtractSegments(string[] moves, List<Segment> horizontals, List<Segment> verticals)
        {
            int x = 0;
            int y = 0;
            long total = 0;
            foreach (string move in moves)
            {
                char direction = move[0];
                int length = int.Parse(move.Substring(1));
                switch (direction)
                {
                    case 'R':
                    {
                        horizontals.Add(new Segment() { X1 = x, Y1 = y, X2 = x + length, Y2 = y, UserData = (x, total)});
                        x += length;
                        break;
                    }

                    case 'L':
                    {
                        horizontals.Add(new Segment() { X1 = x - length, Y1 = y, X2 = x, Y2 = y, UserData = (x, total)});
                        x -= length;
                        break;
                    }

                    case 'U':
                    {
                        verticals.Add(new Segment() { X1 = x, Y1 = y, X2 = x, Y2 = y + length, UserData = (y, total)});
                        y += length;
                        break;
                    }

                    case 'D':
                    {
                        verticals.Add(new Segment() { X1 = x, Y1 = y - length, X2 = x, Y2 = y, UserData = (y, total)});
                        y -= length;
                        break;
                    }
                }

                total += length;
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {                
                // Find the closest one
                int dist = _intersections.Where(p => p.Item1.X != 0 || p.Item1.Y != 0).Min(p => Math.Abs(p.Item1.X) + Math.Abs(p.Item1.Y));
                return dist.ToString();

            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Find the lowest signal delay
                long dist = _intersections.Where(p => p.Item1.X != 0 || p.Item1.Y != 0).Min(p => SignalDelay(p.Item2, p.Item1) + SignalDelay(p.Item3, p.Item1));
                return dist.ToString();
            }

            return "";
        }

        private long SignalDelay(Segment segment, Point intersection)
        {
            var (from, distance) = (ValueTuple<int, long>)segment.UserData;

            if (segment.X1 == segment.X2)
            {
                return distance + Math.Abs(from - intersection.Y);
            }
            else
            {
                return distance + Math.Abs(from - intersection.X);
            }
        }
    }   
}