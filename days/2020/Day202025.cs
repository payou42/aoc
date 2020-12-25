using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202025 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long[] _public;

        private long[] _private;

        public Day202025()
        {
            Codename = "2020-25";
            Name = "Combo Breaker";
        }

        public void Init()
        {
            _public = Aoc.Framework.Input.GetLongVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long key = FindPrivateKey(_public[1], 20201227);
                return Encrypt(_public[0], 20201227, key).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }

        private long FindPrivateKey(long p, long m)
        {
            long current = 7;
            long loopsize = 0;
            while (current != p)
            {
                current = (current * current ) % m;
                loopsize++;
            }

            return loopsize;
        }

        private long Encrypt(long input, long m, long key)
        {
            long current = input;
            for (long k = 0; k < key; ++k)
            {
                current = (current * current ) % m;
            }

            return current;
        }
    }   
}