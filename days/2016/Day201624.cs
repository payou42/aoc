using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201624 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Board<bool> _board;

        private Dictionary<int, Point> _targets;

        private Board<int> _distances;

        public Day201624()
        {
            Codename = "2016-24";
            Name = "Air Duct Spelunking";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
            ParseContent();
            BuildDistances();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                var shortestPath = FindShortestPath(false);
                return $"{shortestPath.Item1}";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                var shortestPath = FindShortestPath(true);
                return $"{shortestPath.Item1}";
            }

            return "";
        }

        private (long, string) FindShortestPath(bool loop)
        {
            // Now we just need to find the shortest path between all targets
            string minPath = "";
            long minLength = long.MaxValue;
            Queue<(string, string)> pathes = new Queue<(string, string)>();
            pathes.Enqueue(("0", string.Join("", _targets.Keys.Select(k => k.ToString()).Skip(1))));
            while (pathes.TryDequeue(out var path))
            {
                if (path.Item2.Length == 0)
                {
                    // Check this path
                    int l = GetPathLength(path.Item1, loop);
                    if (l < minLength)
                    {
                        minLength = l;
                        minPath = path.Item1;
                    }
                }
                else
                {
                    for (int i = 0; i < path.Item2.Length; ++i)
                    {
                        pathes.Enqueue((path.Item1 + path.Item2[i], path.Item2.Remove(i, 1)));
                    }
                }
            }

            return (minLength, minPath);
        }

        private void BuildDistances()
        {
            // Build distances between each targets
            _distances = new Board<int>();
            int max = _targets.Keys.Count();
            foreach (var kvp in _targets)
            {
                // Build the distance map
                Board<int> distanceBoard = new Board<int>();

                // Prepare the distance queue
                Queue<(Point, int)> queue = new Queue<(Point, int)>();
                queue.Enqueue((kvp.Value, 1));

                // Process the queue
                while (queue.TryDequeue(out var item))
                {
                    // Check if location is valid
                    if ((!_board[item.Item1.X, item.Item1.Y]) || (distanceBoard[item.Item1.X, item.Item1.Y] > 0))
                    {
                        continue;
                    }

                    // Mark location
                    distanceBoard[item.Item1.X, item.Item1.Y] = item.Item2;

                    // Enqueue neighbour
                    queue.Enqueue((new Point(item.Item1.X, item.Item1.Y + 1), item.Item2 + 1));
                    queue.Enqueue((new Point(item.Item1.X, item.Item1.Y - 1), item.Item2 + 1));
                    queue.Enqueue((new Point(item.Item1.X + 1, item.Item1.Y), item.Item2 + 1));
                    queue.Enqueue((new Point(item.Item1.X - 1, item.Item1.Y), item.Item2 + 1));
                }

                for (int i = 0; i < max; ++i)
                {
                    _distances[kvp.Key, i] = distanceBoard[_targets[i].X, _targets[i].Y] - 1;
                }
            }
        }

        private int GetPathLength(string path, bool loop)
        {
            int l = 0;
            for (int i = 1; i < path.Length; ++i)
            {
                l += _distances[int.Parse(path[i - 1].ToString()), int.Parse(path[i].ToString())];
            }
            if (loop)
            {
                l += _distances[int.Parse(path[path.Length - 1].ToString()), int.Parse(path[0].ToString())];
            }
            return l;
        }

        private void ParseContent()
        {
            _board = new Board<bool>();
            _targets = new Dictionary<int, Point>();
            for (int y = 0; y < _input.Length; ++y)
            {
                for (int x = 0; x < _input[y].Length; ++x)
                {
                    _board[x, y] = (_input[y][x] != '#');
                    if (char.IsDigit(_input[y][x]))
                    {
                        _targets[int.Parse(_input[y][x].ToString())] = new Point(x, y);
                    }
                }
            }
        }
    }   
}