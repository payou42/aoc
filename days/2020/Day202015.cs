using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202015 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long[] _input;

        public Day202015()
        {
            Codename = "2020-15";
            Name = "Rambunctious Recitation";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetLongVector(this, ",");
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return RunTo(2020).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return RunTo(30000000).ToString();
            }

            return "";
        }

        private long RunTo(long target)
        {
            Dictionary<long, long> ages = new Dictionary<long, long>();
            long turn = 0;
            long last = 0;
            foreach (long n in _input)
            {
                ages[n] = turn;
                turn++;
                last = n;

            }

            long next = 0;
            while (true)
            {
                last = next;
                next = ages.ContainsKey(last) ? (turn - ages[last]) : 0;                    
                ages[last] = turn;
                turn++;
                if (turn == target)
                {
                    return last;
                }
            }
        }
    }
}