using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _18._Duet
{
    class Cpu
    {
        private Queue<Int64> _inbox = null;
        private Queue<Int64> _outbox = null;
        private Registers _registers = null;

        public Queue<Int64> Inbox 
        {
            get
            {
                return _inbox;
            }
        }

        public Queue<Int64> Outbox 
        {
            get
            {
                return _outbox;
            }
        }
        
        public Cpu(Int64 id)
        {
            _inbox = new Queue<Int64>();
            _outbox = new Queue<Int64>();
            _registers = new Registers();
            _registers["p"] = id;
        }

        private Int64 GetValue(string v)
        {
            Int64 output = 0;
            if (Int64.TryParse(v, out output))
            {
                return output;
            }
            return _registers[v];
        }        

        public bool Execute(string[] instructions)
        {
            bool waiting = false;
            string[] instruction = instructions[_registers["ip"]].Split(" ");
            switch (instruction[0])
            {
                case "snd":
                {
                    _outbox.Enqueue(GetValue(instruction[1]));
                    _registers["ip"]++;
                    break;
                }

                case "set":
                {
                    _registers[instruction[1]] = GetValue(instruction[2]);
                    _registers["ip"]++;
                    break;
                }

                case "add":
                {
                    _registers[instruction[1]] += GetValue(instruction[2]);
                    _registers["ip"]++;
                    break;
                }

                case "mul":
                {
                    _registers[instruction[1]] *= GetValue(instruction[2]);
                    _registers["ip"]++;
                    break;
                }

                case "mod":
                {
                    _registers[instruction[1]] %= GetValue(instruction[2]);
                    _registers["ip"]++;
                    break;
                }

                case "rcv":
                {                    
                    if (_inbox.Count > 0)
                    {
                        Int64 v = _inbox.Dequeue();
                        _registers[instruction[1]] = v;
                        _registers["ip"]++;

                    }
                    else
                    {
                        // Stay on this instruction until we have something in the rcv queue
                        waiting = true;
                    }
                    break;
                }

                case "jgz":
                {
                    _registers["ip"] += (GetValue(instruction[1]) > 0) ? GetValue(instruction[2]) : 1;
                    break;
                }                
            }
            return waiting;
        }
    }
}