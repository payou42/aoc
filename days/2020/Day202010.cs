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
                // Counting all arrangements is long. However, all 3-jolts gaps are mandatory,
                // so we know that when there is a 3-jolts gap, the 2 adapters of this gap have to be used no matter what.
                // The last one is also mandatory, because our device is 3 jolts higher than the max adapter.
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
            // Build the list of optional adapters
            var size = end - begin - 1;

            // If there's no optional adapter, then we have a single solution.
            if (size <= 0)
                return 1;
            
            long result = 0;
            for (int j = begin + 1; j <= end; ++j)
            {
                if (_input[j] - _input[begin] <= 3)
                {
                    result += CountCombinations(j, end);
                }
            }

            return result;
        }
    }   
}