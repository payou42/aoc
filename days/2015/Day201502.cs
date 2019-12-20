using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201502 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[][] _input;
        public Day201502()
        {
            Codename = "2015-02";
            Name = "I Was Told There Would Be No Math";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetIntMatrix(this, "x");
            foreach (int[] box in _input)
            {
                Array.Sort(box);
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _input.Select(box => 3*box[0]*box[1] + 2*box[0]*box[2] + 2*box[1]*box[2]).Sum().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _input.Select(box => 2*box[0] + 2*box[1] + box[0]*box[1]*box[2]).Sum().ToString();
            }

            return "";
        }
    }   
}