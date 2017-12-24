using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201710 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201710()
        {            
            Codename = "2017-10";
            Name = "Knot Hash";
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                byte[] input = Input.GetIntVector(this, ",").Select(i => (byte)i).ToArray();
                KnotHash hash = new KnotHash(256, 1);
                hash.Compute(input, false);
                return hash.GetSimpleHash().ToString();
            }

            if (part == Part.Part2)
            {
                KnotHash hash = new KnotHash(256, 64);
                hash.Compute(Input.GetString(this), true);
                return hash.GetDenseHash();
            }

            return "";
        }
    }   
}