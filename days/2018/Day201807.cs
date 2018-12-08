using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201807 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<string, List<string>> _dependencies;

        public Day201807()
        {
            Codename = "2018-07";
            Name = "The Sum of Its Parts";
        }

        public void Init()
        {
            _dependencies = new Dictionary<string, List<string>>();
            foreach (string[] line in Aoc.Framework.Input.GetStringMatrix(this, " "))
            {
                if (!_dependencies.ContainsKey(line[7]))
                {
                    _dependencies[line[7]] = new List<string>();
                }
                if (!_dependencies.ContainsKey(line[1]))
                {
                    _dependencies[line[1]] = new List<string>();
                }                
                _dependencies[line[7]].Add(line[1]);
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                string order = "";
                List<string> tasks = _dependencies.Keys.Select(k => k).OrderBy(k => k).ToList();
                while (tasks.Count > 0)
                {
                    string current = tasks.Where(task => _dependencies[task].Intersect(tasks).Count() == 0).First();
                    tasks.Remove(current);
                    order += current;
                }
                return order;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Dictionary<string, long> times = new Dictionary<string, long>();                
                List<string> tasks = _dependencies.Keys.Select(k => k).OrderBy(k => k).ToList();
                foreach (string t in tasks)
                {
                    times[t] = 0;
                }
                while (tasks.Count > 0)
                {
                    List<string> available = tasks.Where(task => _dependencies[task].Intersect(tasks).Count() == 0).Take(5).ToList();
                    foreach (string t in available)
                    {
                        long startTime = 0;
                        List<long> timings = _dependencies[t].Select(dependency => times[dependency]).ToList();
                        if (timings.Any())
                        {
                            startTime = timings.Max();
                        }
                        times[t] = startTime + 61 + (t[0] - 'A');
                        tasks.Remove(t);
                    }
                }
                
                return times.Values.Max().ToString();
            }

            return "";
        }
    }   
}