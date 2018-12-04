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
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
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

                // Now we just need to find the shortest path between all targets
                string minPath = "";
                int minLength = Int32.MaxValue;

                string currentPath = "".PadLeft(max, '0');
                while (true)
                {
                    // Check the current path
                    if (IsPathValid(currentPath))
                    {
                        // Get path length
                        int l = GetPathLength(currentPath);
                        if (l < minLength)
                        {
                            minLength = l;
                            minPath = currentPath;
                        }
                    }

                    // Generate next path
                    if (!GenerateNextPath(ref currentPath))
                    {
                        break;
                    }
                }

                return "pouet";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }

        private bool IsPathValid(string path)
        {
            for (int i = 0; i < _targets.Keys.Count(); ++i) 
            {
                if (!path.Contains(i.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private int GetPathLength(string path)
        {
            int l = 0;
            for (int i = 1; i < path.Length; ++i)
            {
                l += _distances[path[i - 1], path[i]];
            }
            return l;
        }

        private bool GenerateNextPath(string ref path)
        {
            // todo
            return false;
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