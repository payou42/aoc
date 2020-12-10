using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202010 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long[] _input;

        public Day202010()
        {
            Codename = "2020-10";
            Name = "Adapter Array";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetLongVector(this).OrderBy(l => l).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int jolt1 = 0;
                int jolt3 = 1; // The device is always 3 jolts higher than the max adapter
                for (int i = 1; i < _input.Length; i++)
                {
                    switch (_input[i] - _input[i - 1])
                    {
                        case 1: jolt1++; break;
                        case 3: jolt3++; break;
                    }
                }

                return (jolt1 * jolt3).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Counting all arrangemùents is long. However, all jolt3 gaps are mandatory, so we now than when there
                // is a jolt3 gap, the 2 adapters of this gaps have to be used no matter what
                // The last one is also mandatory
                long combination = 1;
                int previous = 0;
                for (int i = 1; i < _input.Length; i++)
                {
                    if (_input[i] - _input[i - 1] == 3) 
                    {
                        if (previous < i - 1)
                        {
                            combination *= CountCombinations(previous, i - 1);
                        }
                        
                        previous = i;
                    }
                }

                if (previous != _input.Length - 1)
                    combination *= CountCombinations(previous, _input.Length - 1);

                return combination.ToString();
            }

            return "";
        }

        private long CountCombinations(int begin, int end)
        {
            // Build the list of otpional adapters
            var size = end - begin - 1;

            // If there's no optional adapters, then e have a single solution.
            if (size == 0)
                return 1;
            
            long result = 0;
            for (int i = 0; i < Math.Pow(2, size); ++i)
            {         
                // Check if the given chain is valid
                long prev = _input[begin];
                bool valid = true;
                for (int j = begin + 1; j < end; ++j)
                {
                    long mask = (byte)(1 << (j - begin - 1));
                    if ((i & mask) != 0)
                    {
                        if (_input[j] - prev > 3)
                        {
                            valid = false;
                            break;
                        }

                        prev = _input[j];
                    }
                }

                if (_input[end] - prev > 3)
                {
                    valid = false;
                }

                if (valid)
                    result++;
            }
            
            return result;
        }
    }   
}