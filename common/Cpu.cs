using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common
{
    public class Cpu
    {
        public delegate CpuState ExecuteDelegate(Cpu cpu, string[] instruction);

        public Queue<Int64> Inbox { get; private set; }

        public Queue<Int64> Outbox  { get; private set; }

        public Registers Registers  { get; private set; }

        public Registers Counters { get; private set; }

        public CpuState State { get; private set; }
        
        public event ExecuteDelegate OnExecute;

        public Cpu(Int64 id)
        {
            Inbox = new Queue<Int64>();
            Outbox = new Queue<Int64>();
            Registers = new Registers();
            Counters = new Registers();
            State = CpuState.Running;
            
            if (id >= 0)
            {
                Registers["p"] = id;
            }
        }

        public Int64 Resolve(string v)
        {
            Int64 output = 0;
            if (Int64.TryParse(v, out output))
            {
                return output;
            }
            return Registers[v];
        }        

        public CpuState Execute(string[] instructions)
        {
            if ((Registers["ip"] < 0) || (Registers["ip"] >= instructions.Length))
            {
                State = CpuState.Exited;
                return State;
            }

            if (this.OnExecute == null)
            {
                State = CpuState.InvalidInstruction;
                return State;
            }

            string[] instruction = instructions[Registers["ip"]].Split(" ");
            State = this.OnExecute(this, instruction);
            return State;
        }
    }
}