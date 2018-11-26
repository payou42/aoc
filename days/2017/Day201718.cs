using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201718 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        Aoc.Framework.Part _part;

        public Day201718()
        {
            Codename = "2017-18";
            Name = "Duet";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            _part = part;
            if (part == Aoc.Framework.Part.Part1)
            {
                Cpu a = new Cpu(-1);
                a.OnExecute += Execute;

                // Run the program
                while (a.State == Cpu.CpuState.Running)
                {
                    a.Execute(_instructions);
                }
                return a.Registers["freq"].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Cpu a = new Cpu(0);
                Cpu b = new Cpu(1);
                a.OnExecute += Execute;
                b.OnExecute += Execute;

                // Run the program
                bool deadlock = false;
                Int64 sendsCount = 0;
                while (!deadlock)
                {
                    while (a.Outbox.Count > 0)
                    {
                        Int64 v = a.Outbox.Dequeue();
                        b.Inbox.Enqueue(v);
                    }
                    while (b.Outbox.Count > 0)
                    {
                        Int64 v = b.Outbox.Dequeue();
                        a.Inbox.Enqueue(v);
                        sendsCount++;
                    }
                    deadlock = (a.Execute(_instructions) == Cpu.CpuState.Waiting) && (b.Execute(_instructions) == Cpu.CpuState.Waiting);
                }
                return sendsCount.ToString();
            }

            return "";
        }

        private Cpu.CpuState Execute(Cpu cpu, string[] instruction)
        {
            Cpu.CpuState state = Cpu.CpuState.Running;
            switch (instruction[0])
            {
                case "snd":
                {
                    if (_part == Aoc.Framework.Part.Part1)
                    {
                        cpu.Registers["freq"] = cpu.Resolve(instruction[1]);
                    }
                    else
                    {
                        cpu.Outbox.Enqueue(cpu.Resolve(instruction[1]));
                    }                    
                    cpu.Registers["ip"]++;
                    break;
                }

                case "set":
                {
                    cpu.Registers[instruction[1]] = cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "add":
                {
                    cpu.Registers[instruction[1]] += cpu.Resolve(instruction[2]);
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
                    cpu.Registers[instruction[1]] *= cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "mod":
                {
                    cpu.Registers[instruction[1]] %= cpu.Resolve(instruction[2]);
                    cpu.Registers["ip"]++;
                    break;
                }

                case "rcv":
                {
                    if (_part == Aoc.Framework.Part.Part1)
                    {
                        state = Cpu.CpuState.Exited;
                    }
                    else
                    {
                        if (cpu.Inbox.Count > 0)
                        {
                            Int64 v = cpu.Inbox.Dequeue();
                            cpu.Registers[instruction[1]] = v;
                            cpu.Registers["ip"]++;
                        }
                        else
                        {
                            // Stay on this instruction until we have something in the rcv queue
                            state = Cpu.CpuState.Waiting;
                        }
                    }
                    break;
                }

                case "jgz":
                {
                    cpu.Registers["ip"] += (cpu.Resolve(instruction[1]) > 0) ? cpu.Resolve(instruction[2]) : 1;
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