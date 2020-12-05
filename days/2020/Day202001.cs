using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202001 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _input;

        private HashSet<int> _set;

        public Day202001()
        {
            Codename = "2020-01";
            Name = "Report Repair";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetIntVector(this);
            _set = new HashSet<int>(_input);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                foreach (int v in _input)
                {
                    if (_set.Contains(2020 - v))
                    {
                        return (v * (2020 - v)).ToString();
                    }
                }
            
                return "Not found";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                for (int i = 0; i < _input.Length - 2; i++)
                {
                    for (int j = 1; j < _input.Length - 1; ++j)
                    {
                        if (_set.Contains(2020 - _input[i] - _input[j]))
                        {
                            return ((2020 - _input[i] - _input[j]) * _input[i] * _input[j]).ToString();
                        }
                    }
                }
            
                return "Not found";
            }

            return "";
        }
    }   
}