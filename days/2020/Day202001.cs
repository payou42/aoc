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

        public Day202001()
        {
            Codename = "2020-01";
            Name = "Report Repair";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetIntVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                for (int i = 0; i < _input.Length - 1; ++i)
                {
                    for (int j = i + 1; j < _input.Length; ++j)
                    {
                        if (_input[i] + _input[j] == 2020)
                        {
                            return (_input[i] * _input[j]).ToString();
                        }
                    }
                }
            
                return "Not found";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                for (int i = 0; i < _input.Length - 2; ++i)
                {
                    for (int j = i + 1; j < _input.Length - 1; ++j)
                    {
                        for (int k = j + 1; k < _input.Length; ++k)
                        {
                            if (_input[i] + _input[j] + _input[k]== 2020)
                            {
                                return (_input[i] * _input[j] * _input[k]).ToString();
                            }
                        }
                    }
                }
            
                return "Not found";
            }

            return "";
        }
    }   
}