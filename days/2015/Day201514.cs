using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201514 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Reindeer
        {
            public string Name { get; set; }

            public int Speed { get; set; }

            public int TimeRun { get; set; }

            public int TimeRest { get; set; }

            public long GetDistanceAtTime(int t)
            {
                // Full cycles
                long distance = (t / (TimeRun + TimeRest)) * TimeRun * Speed;

                // Last cycle
                distance += Math.Min((t % (TimeRun + TimeRest)), TimeRun) * Speed;

                // All done
                return distance;
            }

        }

        private Reindeer[] _reinders;

        public Day201514()
        {
            Codename = "2015-14";
            Name = "Reindeer Olympics";
        }

        public void Init()
        {
            string[][] input = Aoc.Framework.Input.GetStringMatrix(this, " ");
            _reinders = new Reindeer[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                string[] line = input[i];
                _reinders[i] = new Reindeer
                {
                    Name = line[0],
                    Speed = int.Parse(line[3]),
                    TimeRun = int.Parse(line[6]),
                    TimeRest = int.Parse(line[13])
                };                
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _reinders.Select(r => r.GetDistanceAtTime(2503)).Max().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long[] scores = new long[_reinders.Length];                
                for (int t = 1; t <= 2503; ++t)
                {
                    // Sort reinders
                    var leaderboard = _reinders.Select((r, i) => (i, r.GetDistanceAtTime(t))).OrderByDescending(r => r.Item2);

                    // Get the top score
                    var top = leaderboard.First().Item2;

                    // Only get the firsts
                    var firsts = leaderboard.Where(r => r.Item2 == top).Select(r => r.Item1);

                    // Count the points
                    foreach (int n in firsts)
                    {
                        scores[n]++;
                    }
                }
                return scores.Max().ToString();
            }

            return "";
        }
    }   
}