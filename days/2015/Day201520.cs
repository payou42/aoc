using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201520 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long _input;

        public Day201520()
        {
            Codename = "2015-20";
            Name = "Infinite Elves and Infinite Houses";
        }

        public void Init()
        {
            _input = (long)Aoc.Framework.Input.GetInt(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return GetFirstHouse(_input, 10, 0).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return GetFirstHouse(_input, 11, 50).ToString();
            }

            return "";
        }

        private long GetGiftCount(long house, long elfLimit)
        {
            long given = 0;
            long limit = (long)Math.Ceiling(Math.Sqrt(house));
            for (long i = 1; i < limit; ++i)
            {
                if (house % i != 0)
                {
                    continue;
                }
                long factor1 = i;
                long factor2 = house / i;
                if (elfLimit == 0 || factor2 < elfLimit)
                {
                    given += factor1;
                }
                if (factor1 != factor2 && (elfLimit == 0 || factor1 < elfLimit))
                {
                    given += factor2;
                }
            }
            return given;
        }
        private long GetLowerBound(long target)
        {
            return (long)Math.Ceiling(target / (Math.Exp(0.57721566490153286060651209008240243104215933593992) * Math.Log(Math.Log(target))));
        }

        private long GetUpperBound(long target, long elfLimit)
        {
            long limit = 2;
            uint n = 2;
            while (GetGiftCount(limit, elfLimit) < target)
            {
                n += 1;
                limit *= n;
            }

            for (long from = n; from > 1; --from)
            {
                long boundWithout = limit / from;
                for (long to = 1; to < from; ++to)
                {
                    long boundWith = boundWithout * to;
                    if (GetGiftCount(boundWith, elfLimit) >= target)
                    {
                        limit = boundWith;
                        break;
                    }
                }
            }

            return limit;
        }

        private long GetFirstHouse(long targetPoints, long pointPerElf, long maxElf)
        {
            long target = targetPoints / pointPerElf;
            long from = GetLowerBound(target);
            long to = GetUpperBound(target, maxElf);
            long[] points = new long[to + 1 - from];

            for (long elf = 1; elf <= to; ++elf)
            {
                long skipped = elf < from ? (from - 1) / elf : 0;
                for (long mult = skipped + 1; elf * mult <= to && (maxElf == 0 || mult < maxElf); ++mult)
                {
                    long house = elf * mult;
                    points[house - from] += elf;
                    if (mult == 1 && points[house - from] >= target)
                    {
                        return house;
                    }
                }
            }


            return 0;
        }
    }   
}