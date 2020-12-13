using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Numbers;

namespace Aoc
{
    public class Day202013 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private (long Timestamp, int Index)[] _buses;

        private long _departure;

        public Day202013()
        {
            Codename = "2020-13";
            Name = "Shuttle Search";
        }

        public void Init()
        {
            var input = Aoc.Framework.Input.GetStringVector(this);
            _input = input[1].Split(",");
            _departure = long.Parse(input[0]);
            _buses = _input.Select((value, index) => new { Value = value, Index = index })
                .Where(s => s.Value != "x")
                .Select(s => (long.Parse(s.Value), s.Index)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long bus = 0;
                long mindeparture = long.MaxValue;
                foreach (var b in _buses)
                {
                    long d = (long)Math.Ceiling((double)_departure / (double)b.Timestamp) * b.Timestamp;
                    if (d < mindeparture)
                    {
                        mindeparture = d;
                        bus = b.Timestamp;
                    }
                }

                return (bus * (mindeparture - _departure)).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                List<(long, long)> data = _buses.Select(ni => (ni.Timestamp - (ni.Index % ni.Timestamp), ni.Timestamp)).ToList();
                return ChineseCongruence.Calculate(data).ToString();
            }

            return "";
        }
    }   
}