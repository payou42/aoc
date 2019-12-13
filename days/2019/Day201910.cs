using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201910 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private List<Point> _asteroids;

        private (Point, List<Point>) _best;

        public Day201910()
        {
            Codename = "2019-10";
            Name = "Monitoring Station";
        }

        public void Init()
        {
            _asteroids = new List<Point>();
            string[] lines = Aoc.Framework.Input.GetStringVector(this);
            for (int j = 0; j < lines.Length; ++j)
            {
                for (int i = 0; i < lines[j].Length; ++i)
                {
                    if (lines[j][i] == '#')
                    {
                        _asteroids.Add(new Point(i, j));
                    }
                }
            }

            _best = _asteroids.Select(p => (p, GetVisibles(p))).OrderByDescending(p => p.Item2.Count).First();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {   
                // Count the visible asteroins             
                return _best.Item2.Count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Get the 200th asteroids to be destroyed
                var ordered = _best.Item2.OrderBy(p => GetAngle(_best.Item1, p));
                var nb200 = _best.Item2.OrderBy(p => GetAngle(_best.Item1, p)).Skip(199).First();
                return (nb200.X * 100 + nb200.Y).ToString();
            }

            return "";
        }

        private List<Point> GetVisibles(Point point)
        {
            List<Point> visible = new List<Point>();

            foreach (Point p in _asteroids)
            {
                // Don't count self
                if (p == point)
                {
                    continue;
                }

                // Operation to do
                bool toAdd = true;
                int toRemove = -1;

                // Check against all visible point
                for (int i = 0; i < visible.Count; ++i)
                {
                    // Get the visibe point
                    Point q = visible[i];

                    // Check if they hide themselves
                    int x1 = p.X - point.X;
                    int x2 = q.X - point.X;
                    int y1 = p.Y - point.Y;
                    int y2 = q.Y - point.Y;

                    // Same direction => cross product is zero
                    // Same orientation => dot product is positive
                    if ((y1*x2 - y2*x1 == 0) && (x1*x2 + y1*y2 > 0))
                    {
                        // Keep the closest one
                        if (p.X * p.X + p.Y * p.Y < q.X * q.Y + q.Y * q.Y)
                        {
                            toRemove = i;
                        }
                        else
                        {
                            toAdd = false;
                        }
                    }
                }

                if (toRemove >= 0)
                    visible.RemoveAt(toRemove);

                if (toAdd)
                    visible.Add(p);
            }
            
            return visible;
        }

        private double GetAngle(Point center, Point q)
        {
            double x = q.X - center.X;
            double y = q.Y - center.Y;
            double t = 0.0f;

            if (x == 0)
            {                
                t = (y > 0) ? -double.MaxValue : double.MaxValue;
            }
            else
            {
                t = y / x;
            }

            double angle = Math.PI / 2.0f + Math.Atan(t);
            if (x < 0)
            {
                angle += Math.PI;
            }

            return angle;
        }
    }   
}