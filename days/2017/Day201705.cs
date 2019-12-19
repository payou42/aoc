using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201705 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _input;

        public Day201705()
        {
            Codename = "2017-05";
            Name = "A Maze of Twisty Trampolines, All Alike";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            _input = Aoc.Framework.Input.GetIntVector(this);
            Int32 steps = 0;
            Int32 current = 0;
            while ((current >= 0) && (current < _input.Length))
            {
                Int32 jump = _input[current];
                if (part == Aoc.Framework.Part.Part1)
                {
                    _input[current]++;
                }
                else 
                {
                    if (_input[current] >= 3)
                    {
                        _input[current]--;
                    }
                    else
                    {
                        _input[current]++;
                    }
                }                
                current += jump;
                
                steps++;
            }

            // Finished !
            return steps.ToString();
        }
    }
}