using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201708 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }
        
        private Cpu _cpu;

        private string[] _instructions;

        private Int64 _highest;

        public Day201708()
        {
            Codename = "2017-08";
            Name = "I Heard You Like Registers";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this);
            _cpu = new Cpu(-1);
            _cpu.OnExecute += this.Execute;
            _highest = Int64.MinValue;
            Cpu.CpuState state = Cpu.CpuState.Running;
            while (state == Cpu.CpuState.Running)
            {
                state = _cpu.Execute(_instructions);
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _cpu.Registers.GetLargest().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _highest.ToString();
            }

            return "";
        }

        private bool CheckCondition(string register, string op, Int64 value)
        {
            Int64 r = _cpu.Registers[register];
            switch (op)
            {
                case "==": return r == value;
                case "!=": return r != value;
                case ">": return r > value;
                case "<": return r < value;
                case ">=": return r >= value;
                case "<=": return r <= value;
                default: return true;
            }
        }

        private void ApplyOperation(string register, string op, Int64 value)
        {
            switch (op)
            {
                case "inc":
                {
                    _cpu.Registers[register] += value;                    
                    return;
                }

                case "dec":
                {
                    _cpu.Registers[register] -= value;
                    return;
                }
            }
        }

        private Cpu.CpuState Execute(Cpu cpu, string[] instruction)
        {
            if (CheckCondition(instruction[4], instruction[5], Int64.Parse(instruction[6])))
            {
                ApplyOperation(instruction[0], instruction[1], Int64.Parse(instruction[2]));
                _highest = Math.Max(_highest, _cpu.Registers.GetLargest());
            }
            _cpu.Counters[instruction[1]]++;
            _cpu.Registers["ip"]++;
            return Cpu.CpuState.Running;
        }
    }        
}