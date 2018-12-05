using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;


namespace Aoc
{
    public class Day201625 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        public Day201625()
        {
            Codename = "2016-25";
            Name = "Clock Signal";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long seed = 0;
                long loops = 50000;
                while (!IsClockSignal(seed, loops))
                {
                    seed++;
                }
                return seed.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }

        private bool IsClockSignal(long seed, long testLength)
        {
            // Run CPU
            Cpu cpu = new Cpu(0);
            cpu.OnExecute += OnExecute;
            Cpu.CpuState state = Cpu.CpuState.Running;
            cpu.Registers["a"] = seed;
            while ((state == Cpu.CpuState.Running) && testLength >= 0)
            {
                state = cpu.Execute(_instructions);
                testLength--;
            }

            // Test output
            if (cpu.Outbox.Count == 0)
            {
                return false;
            }
            
            long expected = 0;
            while (cpu.Outbox.TryDequeue(out var tick))
            {
                if (tick != expected)
                {
                    return false;
                }
                expected = 1 - expected;
            }
            return true;
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

                case "out":
                {
                    cpu.Outbox.Enqueue(cpu.Resolve(instruction[1]));
                    break;
                }
            }
            cpu.Registers["ip"]++;
            return Cpu.CpuState.Running;
        }
    }   
}