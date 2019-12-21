using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201510 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201510()
        {
            Codename = "2015-10";
            Name = "Elves Look, Elves Say";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return LookAndSay(_input, 40).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return LookAndSay(_input, 50).ToString();
            }

            return "";
        }

        private long LookAndSay(string input, int iterations)
        {
            // Prepare the data
            List<int> current = input.Select(c => int.Parse(c.ToString())).ToList();

            // Iterate as many times as required
            for (int i = 0; i < iterations; ++i)
            {
                int previous = 0;
                int count = 0;
                List<int> next = new List<int>();

                foreach (int n in current)
                {
                    if ((previous == 0) || (previous == n))
                    {
                        previous = n;
                        count++;
                    }
                    else
                    {
                        next.Add(count);
                        next.Add(previous);
                        previous = n;
                        count = 1;
                    }
                }
                next.Add(count);
                next.Add(previous);
                current = next;
            }

            return current.Count;
        }
    }   
}