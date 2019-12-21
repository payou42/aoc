using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    /// <summary>
    /// An object representing a virtual CPU
    /// </summary>
    public class Cpu
    {
        public enum CpuState
        {
            Running = 0,
            Waiting = 1,
            Exited = 2,
            InvalidInstruction = 3,
        }

        // The exectution callback
        public delegate CpuState ExecuteDelegate(Cpu cpu, string[] instruction);
        public event ExecuteDelegate OnExecute;

        // Incoming messages
        public Queue<Int64> Inbox { get; private set; }

        // Outgoing messages
        public Queue<Int64> Outbox  { get; private set; }

        // Internal registers
        public Registers<Int64> Registers  { get; private set; }

        // Internal counters
        public Registers<Int64> Counters { get; private set; }

        // Current state
        public CpuState State { get; private set; }        

        public Cpu(Int64 id)
        {
            Inbox = new Queue<Int64>();
            Outbox = new Queue<Int64>();
            Registers = new Registers<Int64>();
            Counters = new Registers<Int64>();
            State = CpuState.Running;
            
            if (id >= 0)
            {
                Registers["p"] = id;
            }
        }

        /// <summary>
        /// Resolve an instruction parameter
        /// It can be a direct value (if it's castable in an Int64) or the name of a register
        /// </summary>
        /// <param name="v">The parameter value</param>
        /// <returns>The actual value of the parameter</returns>
        public Int64 Resolve(string v)
        {
            if (Int64.TryParse(v, out long output))
            {
                return output;
            }
            return Registers[v];
        }        

        /// <summary>
        /// Execute a single instruction from a list, according to the "ip" register
        /// </summary>
        /// <param name="instructions">The list of instructions, as strings</param>
        /// <returns>The CPU state after the instruction</returns>
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