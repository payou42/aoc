using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201719 : Day
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
            _tubes = new Tubes(Input.GetStringVector(this));
            _tubes.Walk();
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _tubes.Path.ToString();
            }

            if (part == Part.Part2)
            {
                return _tubes.Steps.ToString();
            }

            return "";
        }
    }   
}