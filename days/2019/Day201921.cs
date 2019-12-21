using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201921 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        public Day201921()
        {
            Codename = "2019-21";
            Name = "Springdroid Adventure";
        }

        public void Init()
        {
            _cpu = new IntCpu();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Load the program
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));

                // Hardcode our springdroid program
                // !A + !C.D
                //   !A
                //     There's a hole next to us, we need to jump no matter what
                //   !C.D
                //     There's a hole in 3 cases and the next one is ground, jump right now.
                //     The idea is to jump as soon as possible when we can.
                string[] program = new string[5]
                {
                    "NOT C T",
                    "AND D T",
                    "NOT A J",
                    "OR T J",
                    "WALK",
                };

                // Enter input
                foreach (string s in program)
                {
                    foreach (char c in s)
                    {
                        _cpu.Input.Enqueue((long)c);
                    }

                    _cpu.Input.Enqueue((long)10);
                }

                // Run the simulation
                _cpu.Run();

                // Show the output
                for (int i = 0; i < 33; ++i)
                {
                    _cpu.Output.Dequeue();
                }

                if (_cpu.Output.Count == 1)
                {
                    return _cpu.Output.Dequeue().ToString();
                }

                while (_cpu.Output.Count > 0)
                {
                    char c = (char)_cpu.Output.Dequeue();
                    if (c == 10)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Load the program
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));

                // Hardcode our springdroid program
                // (!A+!B+!C).D.(E+H)
                //   (!A+!B+!C)
                //       There's at elast a hole in the next 3 cases, we need to jump as soon as possible
                //   D
                //       If we jump now, we'll land on ground
                //   (E+H)
                //       If we need to jump right after lunding (!E) then we'll land on ground again (H).
                //       Or if we can't jump right away (!H), we can just walk (E) 
                string[] program = new string[11]
                {
                    "NOT A T",
                    "NOT B J",
                    "OR J T",
                    "NOT C J",
                    "OR J T",
                    "AND D T",
                    "NOT E J",
                    "NOT J J",
                    "OR H J",
                    "AND T J",
                    "RUN",
                };

                // Enter input
                foreach (string s in program)
                {
                    foreach (char c in s)
                    {
                        _cpu.Input.Enqueue((long)c);
                    }

                    _cpu.Input.Enqueue((long)10);
                }

                // Run the simulation
                _cpu.Run();

                // Show the output
                for (int i = 0; i < 33; ++i)
                {
                    _cpu.Output.Dequeue();
                }

                if (_cpu.Output.Count == 1)
                {
                    return _cpu.Output.Dequeue().ToString();
                }

                while (_cpu.Output.Count > 0)
                {
                    char c = (char)_cpu.Output.Dequeue();
                    if (c == 10)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }
            }

            return "";
        }
    }   
}