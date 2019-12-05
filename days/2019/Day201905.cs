using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201905 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        public Day201905()
        {
            Codename = "2019-05";
            Name = "Sunny with a Chance of Asteroids";
        }

        public void Init()
        {
            _cpu = new IntCpu();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _cpu.Reset(Aoc.Framework.Input.GetIntVector(this, ","));
                _cpu.Input.Enqueue(1);
                _cpu.Run();

                while (_cpu.Output.Count > 0)
                {
                    int result = _cpu.Output.Dequeue();
                    if (_cpu.Output.Count == 0)
                    {
                        return result.ToString();
                    }
                }

                return "Not found";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                _cpu.Reset(Aoc.Framework.Input.GetIntVector(this, ","));
                _cpu.Input.Enqueue(5);
                _cpu.Run();
                return _cpu.Output.Dequeue().ToString();
            }

            return "";
        }
    }   
}