using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201718 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        Part _part;

        public Day201718()
        {            
            Codename = "2017-18";
            Name = "Duet";
            _instructions = Input.GetStringVector(this);
        }

        public string Run(Part part)
        {
            _part = part;
            if (part == Part.Part1)
            {
                Cpu a = new Cpu(-1);
                a.OnExecute += Execute;

                // Run the program
                while (a.State == CpuState.Running)
                {
                    a.Execute(_instructions);
                }
                return a.Registers["freq"].ToString();
            }

            if (part == Part.Part2)
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
                    deadlock = (a.Execute(_instructions) == CpuState.Waiting) && (b.Execute(_instructions) == CpuState.Waiting);
                }
                return sendsCount.ToString();
            }

            return "";
        }

        private CpuState Execute(Cpu cpu, string[] instruction)
        {
            CpuState state = CpuState.Running;
            switch (instruction[0])
            {
                case "snd":
                {
                    if (_part == Part.Part1)
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
                    if (_part == Part.Part1)
                    {
                        state = CpuState.Exited;
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
                            state = CpuState.Waiting;
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