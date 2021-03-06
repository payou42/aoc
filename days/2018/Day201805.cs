using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201805 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201805()
        {
            Codename = "2018-05";
            Name = "Alchemical Reduction";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {                
                return GetPolymerLength(_input).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long shortest = long.MaxValue;
                for (char c = 'a'; c <= 'z'; ++c)
                {
                    string s = c.ToString();
                    string S = s.ToUpper();
                    shortest = Math.Min(shortest, GetPolymerLength(_input.Replace(s, "").Replace(S, "")));
                }
                return shortest.ToString();
            }

            return "";
        }

        private long GetPolymerLength(string polymer)
        {
            int diff = 'a' - 'A';
            Stack<char> stack = new Stack<char>();
            foreach (char c in polymer)
            {
                if (stack.TryPeek(out var p))
                {
                    int v = Math.Abs(p - c);
                    if (v == diff)
                    {
                        stack.Pop();
                        continue;
                    }
                }
                stack.Push(c);
            }

            return stack.Count;
        }
    }
}