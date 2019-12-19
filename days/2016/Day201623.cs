using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201623 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        public Day201623()
        {
            Codename = "2016-23";
            Name = "Safe Cracking";
        }

        public void Init()
        {            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _instructions = Aoc.Framework.Input.GetStringVector(this);
                Cpu cpu = new Cpu(-1);
                cpu.OnExecute += OnExecute;
                Cpu.CpuState state = Cpu.CpuState.Running;
                cpu.Registers["a"] = 7;
                while (state == Cpu.CpuState.Running)
                {
                    state = cpu.Execute(_instructions);
                }
                return cpu.Registers["a"].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                _instructions = Aoc.Framework.Input.GetStringVector(this);
                Cpu cpu = new Cpu(-1);
                cpu.OnExecute += OnExecute;
                Cpu.CpuState state = Cpu.CpuState.Running;
                cpu.Registers["a"] = 12;
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

                case "tgl":
                {
                    long target = cpu.Registers["ip"] + cpu.Resolve(instruction[1]);
                    Patch(target);
                    break;
                }

                case "mul":
                {
                    long a = cpu.Resolve(instruction[1]);
                    long b = cpu.Resolve(instruction[2]);
                    cpu.Registers[instruction[3]] = a * b;
                    break;
                }
            }
            cpu.Registers["ip"]++;
            return Cpu.CpuState.Running;
        }

        private void Patch(long target)
        {
            // If an attempt is made to toggle an instruction outside the program, nothing happens.
            if (target <0 || target >= _instructions.Length)
            {
                return;
            }

            string s = _instructions[target];
            string[] items = s.Split(" ");
            if (items.Length == 2)
            {
                // Single parameter instruction
                // For one-argument instructions, inc becomes dec,
                // and all other one-argument instructions become inc.
                if (items[0] == "inc")
                {                    
                    items[0] = "dec";
                }
                else
                {
                    items[0] = "inc";
                }
            }

            if (items.Length == 3)
            {
                // Two-argument instruction
                // For two-argument instructions,
                // jnz becomes cpy, and all other two-instructions become jnz.
                if (items[0] == "jnz")
                {
                    items[0] = "cpy";
                }
                else
                {
                    items[0] = "jnz";
                }
            }

            // Reassemble the instruction
            _instructions[target] = string.Join(" ", items);
        }
    }   
}