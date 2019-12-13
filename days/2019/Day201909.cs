using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201909 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        public Day201909()
        {
            Codename = "2019-09";
            Name = "Sensor Boost";
        }

        public void Init()
        {
            _cpu = new IntCpu();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                _cpu.Input.Enqueue(1);
                _cpu.Run();
                return _cpu.Output.Dequeue().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                _cpu.Input.Enqueue(2);
                _cpu.Run();
                return _cpu.Output.Dequeue().ToString();
            }

            return "";
        }
    }   
}