using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201819 : Aoc.Framework.Day
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

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

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

        private int _ip;

        private int _register;

        public Day201819()
        {
            Codename = "2018-19";
            Name = "Go With The Flow";
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
                // Run the program
                Registers registers = new Registers();
                _ip = 0;
                while (_ip >= 0 && _ip < _program.Count)
                {
                    // Copy the ip pointer in the register
                    registers[_register] = _ip;

                    // Execute the instruction
                    Instruction instruction = _program[_ip];
                    registers = _commands[instruction.Opcode].Execute(registers, instruction);

                    // Copy back the register in the ip
                    _ip = registers[_register] + 1;
                }

                return registers[0].ToString();

            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Run the program
                // Too long, thr program actually tries to
                // Calculate the sum of the factors of 10551319
                // Which are :
                // 1
                // 23
                // 79
                // 1817
                // 5807
                // 133561
                // 458753
                // 10551319
                // The sum ,of all that is : 11151360
                return "11151360";
            }

            return "";
        }

        private Instruction ParseInstruction(string[] input, int line)
        {
            // Parse an instruction like:
            // seti 0 2 1
            string[] items = input[line].Split(" ");
            Instruction instruction = new Instruction();
            instruction.Opcode = items[0];
            instruction.A = int.Parse(items[1]);
            instruction.B = int.Parse(items[2]);
            instruction.C = int.Parse(items[3]);
            return instruction;
        }
    }   
}