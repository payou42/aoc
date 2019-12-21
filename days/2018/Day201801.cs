using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201801 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _frequencies;

        public Day201801()
        {
            Codename = "2018-01";
            Name = "Chronal Calibration";
        }

        public void Init()
        {
            _frequencies = Aoc.Framework.Input.GetIntVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _frequencies.Sum().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long counter = 0;
                long sum = 0;
                HashSet<long> seen = new HashSet<long> { 0 };
                while (true)
                {
                    sum += _frequencies[counter++ % _frequencies.Length];
                    if (seen.Contains(sum))
                    {
                        return sum.ToString();
                    }
                    seen.Add(sum);
                }
            }

            return "";
        }
    }   
}