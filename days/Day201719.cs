using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201719 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tubes _tubes;

        public Day201719()
        {
            Codename = "2017-19";
            Name = "A Series of Tubes";
        }

        public void Init()
        {
            _tubes = new Tubes(Aoc.Framework.Input.GetStringVector(this));
            _tubes.Walk();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _tubes.Path.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _tubes.Steps.ToString();
            }

            return "";
        }
    }   
}