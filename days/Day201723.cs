using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201723 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        private int _mulCount;

        public Day201723()
        {
            Codename = "2017-23";
            Name = "Digital Plumber";
        }

        public void Init()
        {
            _instructions = Input.GetStringVector(this);
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                _mulCount = 0;
                Cpu a = new Cpu(-1);
                a.OnExecute += Execute;

                // Run the program
                while (a.State == CpuState.Running)
                {
                    a.Execute(_instructions);
                }
                return _mulCount.ToString();
            }

            if (part == Part.Part2)
            {
                int key = Int32.Parse(_instructions[0].Split(" ")[2]);
                int result = 0;
                int start = (key * 100) + 100000;
                int stop  = start + 17000;
                for (int b = start; b <= stop; b += 17)
                {
                    for (int d = 2; d <= Math.Sqrt(b); ++d)
                    {
                        if (b % d == 0)
                        {
                            result++;
                            break;
                        }
                    }
                }
                return result.ToString();
            }

            return "";
        }

        private CpuState Execute(Cpu cpu, string[] instruction)
        {
            CpuState state = CpuState.Running;
            switch (instruction[0])
            {
                case "set":
                {
                    cpu.Registers[instruction[1]] = cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "sub":
                {
                    cpu.Registers[instruction[1]] -= cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "mul":
                {
                    _mulCount++;
                    cpu.Registers[instruction[1]] *= cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "jnz":
                {
                    cpu.Registers["ip"] += (cpu.Resolve(instruction[1]) != 0) ? cpu.Resolve(instruction[2]) : 1;
                    break;
                }
            }
            return state;
        }
    }   
}