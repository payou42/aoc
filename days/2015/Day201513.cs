using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201513 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<string, int> _invited;

        private Dictionary<(int, int), int> _happiness;        

        public Day201513()
        {
            Codename = "2015-13";
            Name = "Knights of the Dinner Table";
        }

        public void Init()
        {
            _invited = new Dictionary<string, int>();
            _happiness = new Dictionary<(int, int), int>();
            string[][] input = Aoc.Framework.Input.GetStringMatrix(this, " ");
            foreach (string[] line in input)
            {
                var last = line[10].Substring(0, line[10].Length - 1);
                if (!_invited.ContainsKey(line[0]))
                {
                    _invited[line[0]] = _invited.Keys.Count;
                }

                if (!_invited.ContainsKey(last))
                {
                    _invited[last] = _invited.Keys.Count;
                }

                int from = _invited[line[0]];
                int to = _invited[last];
                int dist = int.Parse(line[3]) * (line[2] == "lose" ? -1 : 1);
                _happiness[(from, to)] = dist;
            }            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return FindBestHappiness().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int me = _invited.Count;
                _invited["Me"] = me;
                for (int i = 0; i < me; ++i)
                {
                    _happiness[(i, me)] = 0;
                    _happiness[(me, i)] = 0;
                }
                return FindBestHappiness().ToString();
            }

            return "";
        }

        private long FindBestHappiness()
        {
            // Prepare data
            Queue<(List<int>, List<int>)> pathes = new Queue<(List<int>, List<int>)>();
            pathes.Enqueue((new List<int>(), _invited.Values.Select(w => w).ToList()));
            long result = 0;

            // Calculate the shortest path
            while (pathes.TryDequeue(out var path))
            {
                if (path.Item2.Count == 0)
                {
                    // Check this path
                    int l = GetHappiness(path.Item1);
                    result = Math.Max(result, l);
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
            return result;
        }

        private int GetHappiness(List<int> path)
        {
            int l = 0;
            int c = path.Count;
            for (int i = 0; i < path.Count; ++i)
            {
                l += _happiness[(path[i], path[(i - 1 + c) % c])];
                l += _happiness[(path[i], path[(i + 1) % c])];
            }
            return l;
        }
    }   
}