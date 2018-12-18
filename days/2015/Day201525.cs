using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201525 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201525()
        {
            Codename = "2015-25";
            Name = "Let It Snow";
        }

        public void Init()
        {
            // Enter the code at row 2978, column 3083
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long r = 2978;
                long c = 3083;
                long diag = r + c - 1;
                long index = (diag * (diag + 1) / 2) - r + 1;
                long current = 20151125;
                for (long i = 1; i < index; ++i)
                {
                    current = (current * 252533) % 33554393;
                }
                return current.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }
    }   
}