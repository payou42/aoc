using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201501 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public char[] _input;

        public Day201501()
        {
            Codename = "2015-01";
            Name = "Not Quite Lisp";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this).ToCharArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return (_input.Count(c => c =='(') - _input.Count(c => c ==')')).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int floor = 0;
                for (int i = 0; i < _input.Length; ++i)
                {
                    floor += (_input[i] == '(') ? 1 : -1;
                    if (floor == -1)
                    {
                        return (i + 1).ToString();
                    }
                }
            }

            return "";
        }
    }   
}