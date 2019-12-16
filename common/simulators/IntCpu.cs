using System;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    public class IntCpu
    {
        public enum RunningState
        {
            Stopped = 0,

            Halted = 1,
            
            Running = 2,

            WaitingInput = 3,

            WaitingOutput = 4,
        };

        public IntCpu()
        {
            Code = null;
            Input = new Queue<long>();
            Output = new Queue<long>();
            OnOutput = null;
            Ip = 0;
            Rb = 0;
            State = RunningState.Stopped;
            InstructionsSet = new Dictionary<long, Action<long>>();
            LoadDefaultInstructionsSet();
        }

        public delegate void OnOutputDelegate();

        public long[] Code { get; private set; }

        public Queue<long> Input { get; }

        public event OnOutputDelegate OnOutput;

        public Queue<long> Output { get; }

        public long Ip { get; private set; }

        public long Rb { get; private set; }

        public RunningState State { get; set; }

        public bool PauseOnOutput { get; private set; }

        public Dictionary<long, Action<long>> InstructionsSet { get; private set; }

        public void Reset(long[] code)
        {
            Code = code;
            Ip = 0;
            Rb = 0;
            State = RunningState.Stopped;
            PauseOnOutput = false;
            Input.Clear();
            Output.Clear();
        }

        public void Run(bool pauseOnOutput = false)
        {
            State = RunningState.Running;
            PauseOnOutput =  pauseOnOutput;
            while (State == RunningState.Running)
            {
                long op = Read(Ip);
                long opcode = op % 100;
                long modes = op / 100;
                InstructionsSet[opcode](modes);
            }
        }

        private long ReadOperand(int index, long modes)
        {
            long mode = (modes / (long)(Math.Pow(10, index))) % 10;
            if (mode == 0)
            {
                // Position mode
                return Read(Read(Ip + index + 1));
            }

            if (mode == 1)
            {
                // Immediate mode
                return Read(Ip + index + 1);
            }

            if (mode == 2)
            {
                // Relative mode
                return Read(Rb + Read(Ip + index + 1));
            }

            return 0;
        }

        private void WriteOperand(int index, long modes, long value)
        {
            long mode = (modes / (long)(Math.Pow(10, index))) % 10;
            if (mode == 0)
            {
                // Position mode
                long x = Read(Ip + index + 1);
                Write(x, value);
            }

            if (mode == 2)
            {
                // Relative mode
                long x = Rb + Read(Ip + index + 1);
                Write(x, value);
            }
        }

        private void WriteOutput(long v)
        {
            Output.Enqueue(v);
            if (PauseOnOutput)
            {
                this.State = RunningState.WaitingOutput;
            }

            if (this.OnOutput != null)
            {
                OnOutput();
            }
        }

        private void CheckBondaries(long index)
        {
            if (index < 0)
            {
                return;
            }

            if (index >= Code.Length)
            {
                long[] newCode = new long[index * 2];
                Buffer.BlockCopy(Code, 0, newCode, 0, Code.Length * sizeof(long));
                Code = newCode;
            }
        }

        private long Read(long index)
        {
            CheckBondaries(index);
            return Code[index];
        }

        private void Write(long index, long value)
        {
            CheckBondaries(index);
            Code[index] = value;
        }

        private void LoadDefaultInstructionsSet()
        {
            // ADD command
            // Opcode 1 adds together numbers read from two positions and stores the result in a third position.
            // The three integers immediately after the opcode tell you these three positions.
            // The first two indicate the positions from which you should read the input values,
            // and the third indicates the position at which the output should be stored.
            InstructionsSet[1] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                WriteOperand(2, modes, a + b);
                Ip += 4;
            };

            // MUL command
            // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
            // Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
            InstructionsSet[2] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                WriteOperand(2, modes, a * b);
                Ip += 4;
            };

            // LOAD command
            // Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
            // For example, the instruction 3,50 would take an input value and store it at address 50.
            InstructionsSet[3] = (modes) =>
            {
                if (Input.Count == 0)
                {
                    State = RunningState.WaitingInput;
                    return;
                }

                long input = Input.Dequeue();
                WriteOperand(0, modes, input);
                Ip += 2;
            };

            // STORE command
            // Opcode 4 outputs the value of its only parameter.
            // For example, the instruction 4,50 would output the value at address 50.
            InstructionsSet[4] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                WriteOutput(a);
                Ip += 2;
            };

            // JIT command
            // Opcode 5 is jump-if-true: if the first parameter is non-zero,
            // it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
            InstructionsSet[5] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                if (a != 0)
                {
                    Ip = b;
                }
                else
                {
                    Ip += 3;
                }
            };

            // JIF command
            // Opcode 6 is jump-if-false: if the first parameter is zero, it sets the instruction pointer to the 
            // value from the second parameter. Otherwise, it does nothing.
            InstructionsSet[6] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                if (a == 0)
                {
                    Ip = b;
                }
                else
                {
                    Ip += 3;
                }
            };

            // LT command
            // Opcode 7 is less than: if the first parameter is less than the second parameter,
            // it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
            InstructionsSet[7] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                WriteOperand(2, modes, a < b ? 1 : 0);
                Ip += 4;
            };

            // Opcode 8 is equals: if the first parameter is equal to the second parameter,
            // it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
            InstructionsSet[8] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                long b = ReadOperand(1, modes);
                WriteOperand(2, modes, a == b ? 1 : 0);
                Ip += 4;
            };

            // Opcode 9 adjusts the relative base by the value of its only parameter.
            // The relative base increases (or decreases, if the value is negative) by the value of the parameter.
            InstructionsSet[9] = (modes) =>
            {
                long a = ReadOperand(0, modes);
                Rb += a;
                Ip += 2;
            };

            // Stop command
            InstructionsSet[99] = (mode) =>
            {
                Ip += 1;
                State = RunningState.Halted;
            };
        }
    }   
}