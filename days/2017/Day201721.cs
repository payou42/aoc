using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201721 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private RulesSet _rules;

        public Day201721()
        {
            Codename = "2017-21";
            Name = "Fractal Art";
        }
        public void Init()
        {
            _rules = new RulesSet(Aoc.Framework.Input.GetStringVector(this));
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Fractal fractal = new Fractal();
                for (int i = 0; i < 5; ++i)
                {
                    fractal.Iterate(_rules);
                }
                return fractal.CountPixels().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Fractal fractal = new Fractal();
                for (int i = 0; i < 18; ++i)
                {
                    fractal.Iterate(_rules);
                }
                return fractal.CountPixels().ToString();
            }

            return "";
        }
    }   
}