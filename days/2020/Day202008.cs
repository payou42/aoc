using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day202008 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _program;

        public Day202008()
        {
            Codename = "2020-08";
            Name = "Handheld Halting";
        }

        public void Init()
        {
            _program = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Run(out var result);
                return result.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Try to change exactly one instruction at a time
                Dictionary<string, string> map = new() { {"jmp", "nop"}, {"nop", "jmp"} };
                for (int i = 0; i < _program.Length; ++i)
                {
                    if (map.ContainsKey(_program[i][..3]))
                    {
                        _program[i] = map[_program[i][..3]] + _program[i][3..];
                        if (Run(out var result))
                        {
                            return result.ToString();
                        }

                        _program[i] = map[_program[i][..3]] + _program[i][3..];
                    }
                }
            }

            return "";
        }

        private bool Run(out long result)
        {
            HashSet<long> visitedInstructions = new HashSet<long>();
            Cpu cpu = new Cpu(0);
            cpu.OnExecute += OnExecute;

            bool success = true;
            while (cpu.State != Cpu.CpuState.Exited)
            {
                if (visitedInstructions.Contains(cpu.Registers["ip"]))
                {
                    success = false;                    
                    break;
                }

                visitedInstructions.Add(cpu.Registers["ip"]);
                cpu.Execute(_program);
            }

            result = cpu.Registers["acc"];
            return success;
        }

        private Cpu.CpuState OnExecute(Cpu cpu, string[] instruction)
        {
            switch (instruction[0])
            {
                case "acc":
                {
                    cpu.Registers["acc"] += int.Parse(instruction[1]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "jmp":
                {
                    cpu.Registers["ip"] += int.Parse(instruction[1]);
                    break;
                }

                case "nop":
                {
                    cpu.Registers["ip"]++;
                    break;
                }
            }

            return Cpu.CpuState.Running;
        }
    }   
}