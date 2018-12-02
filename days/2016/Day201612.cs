using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201612 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        public Day201612()
        {
            Codename = "2016-12";
            Name = "Leonardo's Monorail";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this, "\n");            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Cpu cpu = new Cpu(-1);
                cpu.OnExecute += OnExecute;
                Cpu.CpuState state = Cpu.CpuState.Running;
                while (state == Cpu.CpuState.Running)
                {
                    state = cpu.Execute(_instructions);
                }
                return cpu.Registers["a"].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Cpu cpu = new Cpu(-1);
                cpu.OnExecute += OnExecute;
                Cpu.CpuState state = Cpu.CpuState.Running;
                cpu.Registers["c"] = 1;
                while (state == Cpu.CpuState.Running)
                {
                    state = cpu.Execute(_instructions);
                }
                return cpu.Registers["a"].ToString();
            }

            return "";
        }

        private Cpu.CpuState OnExecute(Cpu cpu, string[] instruction)
        {
            switch (instruction[0])
            {
                case "cpy":
                {                    
                    cpu.Registers[instruction[2]] = cpu.Resolve(instruction[1]);
                    break;
                }

                case "inc":
                {
                    cpu.Registers[instruction[1]]++;
                    break;
                }

                case "dec":
                {
                    cpu.Registers[instruction[1]]--;
                    break;
                }

                case "jnz":
                {
                    if (cpu.Resolve(instruction[1]) != 0)
                    {
                        cpu.Registers["ip"] += (cpu.Resolve(instruction[2]) - 1);
                    }
                    break;
                }
            }
            cpu.Registers["ip"]++;
            return Cpu.CpuState.Running;
        }
    }   
}