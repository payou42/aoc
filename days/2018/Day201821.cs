using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201821 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum OpCodes
        {
            Addr,
            Addi,
            Mulr,
            Muli,
            Banr,
            Bani,
            Borr,
            Bori,
            Setr,
            Seti,
            Gtir,
            Gtri,
            Gtrr,
            Eqir,
            Eqri,
            Eqrr,
        }

        public class Instruction
        {
            public OpCodes Opcode { get; set; }

            public int A { get; set; }

            public int B { get; set; }

            public int C { get; set; }

            public Action<int[], int, int, int> Action;
        }

        private Instruction[] _program;

        private Dictionary<OpCodes, Action<int[], int, int, int>> _commands;

        private int _register;

        public class Cpu
        {
            public int Ip { get; set; }

            public int[] Registers { get; set; }            
        }

        public Day201821()
        {
            Codename = "2018-21";
            Name = "Chronal Conversion";
        }

        public void Init()
        {
            _commands = new Dictionary<OpCodes, Action<int[], int, int, int>>
            {
                { OpCodes.Addr, (r, a, b, c) => r[c] = r[a] + r[b] },
                { OpCodes.Addi, (r, a, b, c) => r[c] = r[a] + b },
                { OpCodes.Mulr, (r, a, b, c) => r[c] = r[a] * r[b] },
                { OpCodes.Muli, (r, a, b, c) => r[c] = r[a] * b },
                { OpCodes.Banr, (r, a, b, c) => r[c] = r[a] & r[b] },
                { OpCodes.Bani, (r, a, b, c) => r[c] = r[a] & b },
                { OpCodes.Borr, (r, a, b, c) => r[c] = r[a] | r[b] },
                { OpCodes.Bori, (r, a, b, c) => r[c] = r[a] | b },
                { OpCodes.Setr, (r, a, b, c) => r[c] = r[a] },
                { OpCodes.Seti, (r, a, b, c) => r[c] = a },
                { OpCodes.Gtir, (r, a, b, c) => r[c] = (a > r[b]) ? 1 : 0 },
                { OpCodes.Gtri, (r, a, b, c) => r[c] = (r[a] > b) ? 1 : 0 },
                { OpCodes.Gtrr, (r, a, b, c) => r[c] = (r[a] > r[b]) ? 1 : 0 },
                { OpCodes.Eqir, (r, a, b, c) => r[c] = (a == r[b]) ? 1 : 0 },
                { OpCodes.Eqri, (r, a, b, c) => r[c] = (r[a] == b) ? 1 : 0 },
                { OpCodes.Eqrr, (r, a, b, c) => r[c] = (r[a] == r[b]) ? 1 : 0 }
            };

            string[] input = Aoc.Framework.Input.GetStringVector(this).Where(l => !string.IsNullOrEmpty(l)).ToArray();
            _register = int.Parse(input[0].Substring(4));
            _program = input.Skip(1).Select(line => ParseInstruction(line)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Prepare the Cpu
                Cpu cpu = new Cpu { Registers = new int[6], Ip = 0};
                    
                // Run the program
                while (true)
                {
                    if (cpu.Ip == 28)
                    {
                        return cpu.Registers[3].ToString();
                    }

                    // Copy the ip pointer in the register
                    cpu.Registers[_register] = cpu.Ip;

                    // Execute the instruction
                    Instruction instruction = _program[cpu.Ip];
                    instruction.Action(cpu.Registers, instruction.A, instruction.B, instruction.C);

                    // Copy back the register in the ip
                    cpu.Ip = cpu.Registers[_register] + 1;
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // prepare the Cpu
                Cpu cpu = new Cpu { Registers = new int[6], Ip = 0};

                // Values of R3
                HashSet<int> history = new HashSet<int>();                
                int last = 0;
                    
                // Run the program
                while (true)
                {
                    // Check halt condition
                    if (cpu.Ip == 28)
                    {
                        // Check r3
                        int r3 = cpu.Registers[3];
                        if (history.Contains(r3))
                        {
                            return last.ToString();
                        }

                        history.Add(r3);
                        last = r3;
                    }

                    // Copy the ip pointer in the register
                    cpu.Registers[_register] = cpu.Ip;

                    // Execute the instruction
                    Instruction instruction = _program[cpu.Ip];
                    instruction.Action(cpu.Registers, instruction.A, instruction.B, instruction.C);

                    // Copy back the register in the ip
                    cpu.Ip = cpu.Registers[_register] + 1;
                }
            }

            return "";
        }

        private Instruction ParseInstruction(string line)
        {
            // Parse an instruction like:
            // seti 0 2 1
            string[] items = line.Split(" ");
            OpCodes opcode;
            Enum.TryParse(items[0], true, out opcode);
            Instruction instruction = new Instruction
            {
                Opcode = opcode,
                A = int.Parse(items[1]),
                B = int.Parse(items[2]),
                C = int.Parse(items[3]),
                Action = _commands[opcode],
            };

            return instruction;
        }
    }   
}