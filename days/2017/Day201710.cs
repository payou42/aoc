using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201710 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201710()
        {
            Codename = "2017-10";
            Name = "Knot Hash";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                byte[] input = Aoc.Framework.Input.GetIntVector(this, ",").Select(i => (byte)i).ToArray();
                KnotHash hash = new KnotHash(256, 1);
                hash.Compute(input, false);
                return hash.GetSimpleHash().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                KnotHash hash = new KnotHash(256, 64);
                hash.Compute(Aoc.Framework.Input.GetString(this), true);
                return hash.GetDenseHash();
            }

            return "";
        }
    }   
}