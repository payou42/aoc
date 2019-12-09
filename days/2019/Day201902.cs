using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201902 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        public Day201902()
        {
            Codename = "2019-02";
            Name = "1202 Program Alarm";
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
                _cpu.Code[1] = 12;
                _cpu.Code[2] = 2;
                _cpu.Run();
                return _cpu.Code[0].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                for (int noun = 0; noun <= 99; noun++)
                {
                    for (int verb = 0; verb <= 99; verb++)
                    {
                        _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                        _cpu.Code[1] = noun;
                        _cpu.Code[2] = verb;
                        _cpu.Run();
                        if (_cpu.Code[0] == 19690720)
                        {
                            return (100 * noun + verb).ToString();
                        }
                    }
                }
                return "Not found";
            }

            return "";
        }
    }   
}