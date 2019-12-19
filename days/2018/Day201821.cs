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

        public class Registers : IEquatable<Registers>
        {
            public int[] Values { get; set; }

            public Registers()
            {
                Values = new int[6];
            }

            public Registers(Registers other)
            {
                Values = other.Values.ToArray();
            }

            public int this[int index]
            {
                get
                {
                    return Values[index];
                }

                set
                {
                    Values[index] = value;
                }
            }

            public override bool Equals(Object other)
            {
                if ((other == null) || other.GetType() != typeof(Registers))
                {
                    return false;
                }

                return Equals((Registers)other);
            }

            public override int GetHashCode() => base.GetHashCode();

            public bool Equals(Registers other)
            {
                if (other == null)
                {
                    return false;
                }

                for (int i = 0; i < 6; ++i)
                {
                    if (Values[i] != other.Values[i])
                    {
                        return false;
                    }
                }
                
                return true;
            }

            public static bool operator == (Registers r1, Registers r2)
            {
                if (((object)r1) == null || ((object)r2) == null)
                    return Object.Equals(r1, r2);

                return r1.Equals(r2);
            }


            public static bool operator != (Registers r1, Registers r2)
            {
                if (((object)r1) == null || ((object)r2) == null)
                    return ! Object.Equals(r1, r2);

                return !(r1.Equals(r2));
            }
        }

        public class Command
        {
            public string Name { get; set; }

            public Func<Registers, int, int, int> Executor { get; set; }

            public Command(string name, Func<Registers, int, int, int> executor)
            {
                Name = name;
                Executor = executor;
            }

            public Registers Execute(Registers before, Instruction i)
            {
                Registers after = new Registers(before);
                after[i.C] = Executor(before, i.A, i.B);
                return after;
            }
        }

        public class Instruction
        {
            public string Opcode { get; set; }

            public int A { get; set; }

            public int B { get; set; }

            public int C { get; set; }
        }

        private List<Instruction> _program;

        private Dictionary<string, Command> _commands;

        private int _register;


        public class Cpu
        {
            public int Ip { get; set; }

            public Registers Registers { get; set; }            
        }

        public Day201821()
        {
            Codename = "2018-21";
            Name = "Chronal Conversion";
        }

        public void Init()
        {
            _commands = new Dictionary<string, Command>
            {
                { "addr", new Command("addr", (r, a, b) => r[a] + r[b]) },
                { "addi", new Command("addi", (r, a, b) => r[a] + b) },
                { "mulr", new Command("mulr", (r, a, b) => r[a] * r[b]) },
                { "muli", new Command("muli", (r, a, b) => r[a] * b) },
                { "banr", new Command("banr", (r, a, b) => r[a] & r[b]) },
                { "bani", new Command("bani", (r, a, b) => r[a] & b) },
                { "borr", new Command("borr", (r, a, b) => r[a] | r[b]) },
                { "bori", new Command("bori", (r, a, b) => r[a] | b) },
                { "setr", new Command("setr", (r, a, b) => r[a]) },
                { "seti", new Command("seti", (r, a, b) => a) },
                { "gtir", new Command("gtir", (r, a, b) => (a > r[b]) ? 1 : 0) },
                { "gtri", new Command("gtri", (r, a, b) => (r[a] > b) ? 1 : 0) },
                { "gtrr", new Command("gtrr", (r, a, b) => (r[a] > r[b]) ? 1 : 0) },
                { "eqir", new Command("eqir", (r, a, b) => (a == r[b]) ? 1 : 0) },
                { "eqri", new Command("eqri", (r, a, b) => (r[a] == b) ? 1 : 0) },
                { "eqrr", new Command("eqrr", (r, a, b) => (r[a] == r[b]) ? 1 : 0) }
            };

            _program = new List<Instruction>();
            string[] input = Aoc.Framework.Input.GetStringVector(this).Where(l => !string.IsNullOrEmpty(l)).ToArray();
            int line = 0;
            while (line < input.Length)
            {
                if (input[line].StartsWith("#ip"))
                {
                    _register = int.Parse(input[line].Substring(4));
                    line++;
                }
                else
                {
                    Instruction instruction = ParseInstruction(input, line);
                    _program.Add(instruction);
                    line += 1;
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Prepare the Cpu
                Cpu cpu = new Cpu { Registers = new Registers(), Ip = 0};
                    
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
                    cpu.Registers = _commands[instruction.Opcode].Execute(cpu.Registers, instruction);

                    // Copy back the register in the ip
                    cpu.Ip = cpu.Registers[_register] + 1;
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // prepare the Cpu
                Cpu cpu = new Cpu { Registers = new Registers(), Ip = 0};

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
                        if (history.Contains(cpu.Registers[3]))
                        {
                            return last.ToString();
                        } 
                        history.Add(cpu.Registers[3]);
                        last = cpu.Registers[3];
                    }

                    // Copy the ip pointer in the register
                    cpu.Registers[_register] = cpu.Ip;

                    // Execute the instruction
                    Instruction instruction = _program[cpu.Ip];
                    cpu.Registers = _commands[instruction.Opcode].Execute(cpu.Registers, instruction);

                    // Copy back the register in the ip
                    cpu.Ip = cpu.Registers[_register] + 1;
                }
            }

            return "";
        }

        private Instruction ParseInstruction(string[] input, int line)
        {
            // Parse an instruction like:
            // seti 0 2 1
            string[] items = input[line].Split(" ");
            Instruction instruction = new Instruction
            {
                Opcode = items[0],
                A = int.Parse(items[1]),
                B = int.Parse(items[2]),
                C = int.Parse(items[3])
            };
            return instruction;
        }
    }   
}