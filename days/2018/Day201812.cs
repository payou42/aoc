using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201812 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private readonly string _initial = "##..#..##....#..#..#..##.#.###.######..#..###.#.#..##.###.#.##..###..#.#..#.##.##..###.#.#...#.##..";

        private Dictionary<string, char> _rules;

        public Day201812()
        {
            Codename = "2018-12";
            Name = "Subterranean Sustainability";
        }

        public void Init()
        {
            _rules = new Dictionary<string, char>();
            foreach (string[] line in Aoc.Framework.Input.GetStringVector(this).Select(line => line.Split(" => ")))
            {
                _rules[line[0]] = line[1][0];
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                string current = _initial;
                long first = 0;
                for (int i = 0; i < 20; ++i)
                {
                    current = Grow(current);
                    first -= 2;
                }

                return Count(current, first).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // First iterate 1000 times
                string current = _initial;
                long first = 0;
                for (int i = 0; i < 999; ++i)
                {
                    current = Grow(current);
                    first -= 2;
                }

                // Then measure the difference on the next iteration
                long countAfter999 = Count(current, first);
                current = Grow(current);
                first -= 2;
                long countAfter1000 = Count(current, first);

                // Then extrapolate that
                long total = 50000000000;
                return (countAfter1000 + (countAfter1000 - countAfter999) * (total - 1000)).ToString();
            }

            return "";
        }

        private long Count(string current, long first)
        {
            long index = first;
            long count = 0;
            for (int i = 0; i < current.Length; ++i)
            {
                if (current[i] == '#')
                {
                    count += index;
                }
                index++;
            }
            return count;
        }

        private string Grow(string state)
        {
            string current = "...." + state + "....";            
            StringBuilder next = new StringBuilder();
            for (int i = 2; i < current.Length - 2; ++i)
            {
                next.Append(_rules.TryGetValue(current.Substring(i - 2, 5), out var c) ? c : '.');
            }
            return next.ToString();
        }
    }   
}