using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201509 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<string, int> _waypoints;

        private Dictionary<(int, int), int> _distances;

        public Day201509()
        {
            Codename = "2015-09";
            Name = "All in a Single Night";
        }

        public void Init()
        {
            _waypoints = new Dictionary<string, int>();
            _distances = new Dictionary<(int, int), int>();
            string[][] input = Aoc.Framework.Input.GetStringMatrix(this, " ");
            foreach (string[] line in input)
            {
                if (!_waypoints.ContainsKey(line[0]))
                {
                    _waypoints[line[0]] = _waypoints.Keys.Count;
                }

                if (!_waypoints.ContainsKey(line[2]))
                {
                    _waypoints[line[2]] = _waypoints.Keys.Count;
                }

                int from = _waypoints[line[0]];
                int to = _waypoints[line[2]];
                int dist = int.Parse(line[4]);
                _distances[(from, to)] = dist;
                _distances[(to, from)] = dist;
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return FindPathLength(true).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return FindPathLength(false).ToString();
            }

            return "";
        }

        private string FindPathLength(bool shortest)
        {
            // Prepare data
            Queue<(List<int>, List<int>)> pathes = new Queue<(List<int>, List<int>)>();
            pathes.Enqueue((new List<int>(), _waypoints.Values.Select(w => w).ToList()));
            long result = shortest ? long.MaxValue : 0;

            // Calculate the shortest path
            while (pathes.TryDequeue(out var path))
            {
                if (path.Item2.Count == 0)
                {
                    // Check this path
                    int l = GetPathLength(path.Item1);
                    result = shortest ? Math.Min(result, l) : Math.Max(result, l);
                }
                else
                {
                    // Enqueue all possible moves
                    foreach (int next in path.Item2)
                    {
                        List<int> newPath = path.Item1.Select(o => o).ToList();
                        newPath.Add(next);
                        List<int> newRemain = path.Item2.Where(o => o != next).Select(o => o).ToList();
                        pathes.Enqueue((newPath, newRemain));
                    }
                }
            }
            return result.ToString();
        }

        private int GetPathLength(List<int> path)
        {
            int l = 0;
            for (int i = 1; i < path.Count; ++i)
            {
                l += _distances[(path[i - 1], path[i])];
            }
            return l;
        }
    }   
}