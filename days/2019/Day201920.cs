using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;
using System.IO;

namespace Aoc
{
    public class Day201920 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<char> _board;

        private Dictionary<Point, (Point, int)> _portals;

        private Point _start;

        private Point _end;

        private struct State
        {
            public State(Point origin)
            {
                Position = origin;
                Distance = 0;
                Level = 0;
            }

            public Point Position { get; set; }

            public ushort Level { get; set; }

            public long Distance { get; set; }

            public long Hash => (long)((long)Level << 32) + (long)(Position.X << 16) + (long)(Position.Y);
        }

        public Day201920()
        {
            Codename = "2019-20";
            Name = "Donut Maze";
        }

        public void Init()
        {
            _board = new Board<char>();
            _portals = new Dictionary<Point, (Point, int)>();
            Dictionary<string, List<Point>> entrances = new Dictionary<string, List<Point>>();
            var lines = Aoc.Framework.Input.GetStringVector(this);
            for (int h = 0; h < lines.Length; ++h)
            {
                var line = lines[h];
                for (int w = 0; w < line.Length; ++w)
                {
                    _board[w, h] = line[w] == '.' ? '.' : '#';
                    if (char.IsUpper(line[w]))
                    {
                        char a = line[w];
                        char b = (char)0;
                        Point p = new Point(0,0);

                        // Look for another upper case nearby
                        if (w < line.Length - 1 && char.IsUpper(line[w + 1]))
                        {
                            b = line[w + 1];
                            p = new Point((w < line.Length - 2 && line[w + 2] == '.') ? w + 2 : w - 1, h);
                        }
                        else if (h < lines.Length - 1 && char.IsUpper(lines[h + 1][w]))
                        {
                            b = lines[h + 1][w];
                            p = new Point(w, (h < lines.Length - 2 && lines[h + 2][w] == '.') ? h + 2 : h - 1);
                        }

                        if (b != (char)0)
                        {
                            string key = (a > b) ? $"{b}{a}" : $"{a}{b}";
                            if (!entrances.ContainsKey(key))
                                entrances[key] = new List<Point>();
                            
                            entrances[key].Add(p);
                        }
                    }
                }
            }

            // Build the portals
            foreach (var kv in entrances)
            {
                if (kv.Key == "AA")
                {
                    _start = kv.Value[0];
                    continue;
                }

                if (kv.Key == "ZZ")
                {
                    _end = kv.Value[0];
                    continue;
                }

                bool firstOuter = IsOuter(kv.Value[0], lines[0].Length, lines.Length);
                _portals[kv.Value[0]] = (kv.Value[1], firstOuter ? -1 : 1);
                _portals[kv.Value[1]] = (kv.Value[0], firstOuter ? 1 : -1);
            }
        }

        private bool IsOuter(Point point, int w, int h)
        {
            return point.X == 2 || point.X == (w - 3) || point.Y == 2 || point.Y == (h -3);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Run DFS
                long steps = ShortestPath(_start, _end, 0);
                return steps.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Run DFS but with using levels
                long steps = ShortestPath(_start, _end, 1);
                return steps.ToString();
            }

            return "";
        }

        private long ShortestPath(Point origin, Point destination, int levelIncrement)
        {
            // Clear history
            HashSet<long> history = new HashSet<long>();

            // Prepare initial state
            Queue<State> queue = new Queue<State>();
            queue.Enqueue(new State(origin));

            while (queue.Count > 0)
            {
                // Get  the state to process
                var current = queue.Dequeue();

                // Abort if we've already been here
                if (history.Contains(current.Hash))
                    continue;
                history.Add(current.Hash);

                // Check if it's a final state
                if ((current.Position == destination) && (current.Level == 0))
                    return current.Distance;
                
                // Add the next states using portals
                if (_portals.ContainsKey(current.Position))
                {
                    var portal = _portals[current.Position];
                    int level = current.Level + (levelIncrement * portal.Item2);
                    if (level >= 0)
                    {
                        queue.Enqueue(new State(portal.Item1) { Distance = current.Distance + 1, Level = (ushort)level });
                    }
                }

                // Add the next states using regular moves
                for (int d = 0; d < (int)Direction.Count; ++d)
                {
                    Point np = Board<long>.MoveForward(current.Position, (Direction)d);
                    char nc = _board[np];
                    if (nc == '#')
                        continue;

                    queue.Enqueue(new State(np) { Distance = current.Distance + 1, Level = current.Level });
                }
            }

            return 0;
        }
    }
}