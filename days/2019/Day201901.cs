using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201901 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _modules;

        public Day201901()
        {
            Codename = "2019-01";
            Name = "The Tyranny of the Rocket Equation";
        }

        public void Init()
        {
            _modules = Aoc.Framework.Input.GetIntVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _modules.Sum(m => this.GetFuel1(m)).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _modules.Sum(m => this.GetFuel2(m)).ToString();
            }

            return "";
        }

        public int GetFuel1(int mass)
        {
            return (int)Math.Floor((double)mass / 3) - 2;
        }

        public int GetFuel2(int mass)
        {
            int fuel = (int)Math.Floor((double)mass / 3) - 2;
            if (fuel > 0)
            {
                fuel += this.GetFuel2(fuel);
            }

            return Math.Max(0, fuel);
        }
    }   
}