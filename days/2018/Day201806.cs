using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201806 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201806()
        {
            Codename = "2018-06";
            Name = "Chronal Coordinates";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return "part1";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }
    }   
}