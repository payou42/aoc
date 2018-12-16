using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201816 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Registers : IEquatable<Registers>
        {
            public int[] Values { get; set; }

            public Registers()
            {
                Values = new int[4];
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

            public bool Equals(Registers other)
            {
                if (other == null)
                {
                    return false;
                }

                for (int i = 0; i < 4; ++i)
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
            public int Opcode { get; set; }

            public int A { get; set; }

            public int B { get; set; }

            public int C { get; set; }
        }

        public class Sample
        {
            public Registers Before { get; set; }

            public Registers After { get; set; }

            public Instruction Instruction { get; set; }

            public Sample()
            {
                Before = new Registers();
                After = new Registers();
                Instruction = new Instruction();
            }
        }

        private List<Sample> _samples;

        private List<Instruction> _program;

        private List<Command> _commands;

        public Day201816()
        {
            Codename = "2018-16";
            Name = "Chronal Classification";
        }

        public void Init()
        {
            _commands = new List<Command>
            {
                new Command("addr", (r, a, b) => r[a] + r[b]),
                new Command("addi", (r, a, b) => r[a] + b),
                new Command("mulr", (r, a, b) => r[a] * r[b]),
                new Command("muli", (r, a, b) => r[a] * b),
                new Command("banr", (r, a, b) => r[a] & r[b]),
                new Command("bani", (r, a, b) => r[a] & b),
                new Command("borr", (r, a, b) => r[a] | r[b]),
                new Command("bori", (r, a, b) => r[a] | b),
                new Command("setr", (r, a, b) => r[a]),
                new Command("seti", (r, a, b) => a),
                new Command("gtir", (r, a, b) => (a > r[b]) ? 1 : 0),
                new Command("gtri", (r, a, b) => (r[a] > b) ? 1 : 0),
                new Command("gtrr", (r, a, b) => (r[a] > r[b]) ? 1 : 0),
                new Command("eqir", (r, a, b) => (a == r[b]) ? 1 : 0),
                new Command("eqri", (r, a, b) => (r[a] == b) ? 1 : 0),
                new Command("geqr", (r, a, b) => (r[a] == r[b]) ? 1 : 0)
            };

            _samples = new List<Sample>();
            _program = new List<Instruction>();
            string[] input = Aoc.Framework.Input.GetStringVector(this).Where(l => !string.IsNullOrEmpty(l)).ToArray();
            int line = 0;
            while (line < input.Length)
            {
                if (input[line].StartsWith("Before:"))
                {
                    Sample sample = ParseSample(input, line);
                    _samples.Add(sample);
                    line += 3;
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
                // Sample counters
                int count = _samples.Count(sample => _commands.Count(command => command.Execute(sample.Before, sample.Instruction) == sample.After) >= 3);

                // All done, already ??
                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Build the matches
                Dictionary<int, List<Command>> matches = new Dictionary<int, List<Command>>();

                // Parse each sample and eliminate commands not matching
                foreach (Sample sample in _samples)
                {
                    // Initialize each opcode with a full list
                    if (!matches.ContainsKey(sample.Instruction.Opcode))
                    {
                        matches[sample.Instruction.Opcode] = _commands.ToList();
                    }

                    // Reduce the scope of each opcode
                    matches[sample.Instruction.Opcode] = matches[sample.Instruction.Opcode].Intersect(_commands.Where(command => command.Execute(sample.Before, sample.Instruction) == sample.After)).ToList();
                }

                Dictionary<int, Command> _assembler = new Dictionary<int, Command>();
                int countSolved = -1;
                while (_assembler.Keys.Count != countSolved)
                {
                    // Update counter
                    countSolved = _assembler.Keys.Count;

                    // Mark the easy one
                    var resolved = matches.Where(item => item.Value.Count == 1).Select(item => (Key: item.Key, Value: item.Value[0])).ToList();
                    foreach (var item in resolved)
                    {
                        _assembler[item.Key] = item.Value;
                    }

                    // Remove them from the list
                    foreach (List<Command> v in matches.Values.ToList())
                    {
                        foreach (var c in resolved)
                        {
                            v.Remove(c.Value);
                            if (v.Count == 0)
                            {
                                matches.Remove(c.Key);
                            }
                        }
                    }
                }

                // Now that we have the assembler, run the program !
                Registers registers = new Registers();
                foreach(Instruction instruction in _program)
                {
                    registers = _assembler[instruction.Opcode].Execute(registers, instruction);
                }

                return registers[0].ToString();
            }

            return "";
        }

        private Sample ParseSample(string[] input, int line)
        {
            // Parse a sample like:
            // Before: [0, 3, 3, 0]
            // 5 0 2 1
            // After:  [0, 0, 3, 0]
            Sample sample = new Sample();

            // Parse before registers
            string[] before = input[line].Split('[', ',', ']');
            for (int i = 0; i < 4; ++i)
            {
                sample.Before[i] = int.Parse(before[i + 1].Trim());
            }

            // Parse instruction
            sample.Instruction = ParseInstruction(input, line + 1);
            
            // Parse after registers
            string[] after = input[line + 2].Split('[', ',', ']');
            for (int i = 0; i < 4; ++i)
            {
                sample.After[i] = int.Parse(after[i + 1].Trim());
            }

            return sample;
        }
        
        private Instruction ParseInstruction(string[] input, int line)
        {
            // Parse an instruction like:
            // 5 0 2 1
            string[] items = input[line].Split(" ");
            Instruction instruction = new Instruction();
            instruction.Opcode = int.Parse(items[0]);
            instruction.A = int.Parse(items[1]);
            instruction.B = int.Parse(items[2]);
            instruction.C = int.Parse(items[3]);
            return instruction;
        }
    }
}