using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202003 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private int _width;

        public Day202003()
        {
            Codename = "2020-03";
            Name = "Toboggan Trajectory";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
            _width = _input[0].Length;
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return CountTrees(3, 1).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long product = 1;
                product *= CountTrees(1, 1);
                product *= CountTrees(3, 1);
                product *= CountTrees(5, 1);
                product *= CountTrees(7, 1);
                product *= CountTrees(1, 2);
                return product.ToString();
            }

            return "";
        }

        private long CountTrees(int xslope, int yslope)
        {
            long count = 0;
            int x = 0;
            int y = 0;
            while (y < _input.Length)
            {
                count += (long)(_input[y][x % _width] == '#' ? 1 : 0);
                y += yslope;
                x += xslope;
            }

            return count;
        }
    }   
}