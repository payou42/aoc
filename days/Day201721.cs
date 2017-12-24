using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201721 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private RulesSet _rules;

        public Day201721()
        {            
            Codename = "2017-21";
            Name = "Fractal Art";
            _rules = new RulesSet(Input.GetStringVector(this));
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                Fractal fractal = new Fractal();
                for (int i = 0; i < 5; ++i)
                {
                    fractal.Iterate(_rules);
                }
                return fractal.CountPixels().ToString();
            }

            if (part == Part.Part2)
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