using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202009 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long[] _input;

        private long _weakness;

        public Day202009()
        {
            Codename = "2020-09";
            Name = "Encoding Error";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetLongVector(this);
            for (int i = 25; i < _input.Length; ++i)
            {
                HashSet<long> preamble = new HashSet<long>(_input[(i - 25)..i]);
                if (!IsValid(_input[i], preamble))
                {
                    _weakness = _input[i];
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _weakness.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                List<(long Sum, long Min, long Max)> sums = new List<(long Sum, long Min, long Max)>();
                for (int i = 0; i < _input.Length; ++i)
                {
                    // Get the value
                    long v = _input[i];

                    // Append the ith value to each element of the list
                    for (int l = sums.Count - 1; l >= 0; l--)
                    {
                        var s = sums[l];
                        if (s.Sum + v == _weakness)
                        {
                            // We found the solution
                            return (Math.Min(s.Min, v) + Math.Max(s.Max, v)).ToString();
                        }
                        else if (s.Sum + v > _weakness)
                        {
                            // Delete this entry, the sum is too big
                            sums.RemoveAt(l);
                        }
                        else
                        {
                            // Sum is still too low, update the entry
                            sums[l] = (s.Sum + v, Math.Min(s.Min, v), Math.Max(s.Max, v));
                        }
                    }

                    // Start a new entry
                    sums.Add((v, v, v));
                }
            }

            return "";
        }

        private bool IsValid(long number, HashSet<long> preamble)
        {
            foreach (long i in preamble)
            {
                if (preamble.Contains(number - i) && (number - i != i))
                {
                    return true;
                }
            }

            return false;
        }
    }   
}