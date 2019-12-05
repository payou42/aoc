using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    public class IntCpu
    {

        public IntCpu()
        {
            Code = null;
            Input = new Queue<int>();
            Output = new Queue<int>();
            Ip = 0;
            Running = false;
            InstructionsSet = new Dictionary<int, Action<int>>();
            LoadDefaultInstructionsSet();
        }

        public int[] Code { get; private set; }

        public Queue<int> Input { get; }

        public Queue<int> Output { get; }

        public int Ip { get; private set; }

        public bool Running { get; private set; }

        public Dictionary<int, Action<int>> InstructionsSet { get; private set; }

        public void Reset(int[] code)
        {
            Code = code;
            Ip = 0;
            Running = false;
            Input.Clear();
            Output.Clear();
        }

        public void Run()
        {
            Running = true;
            while (Running)
            {
                int op = Code[Ip];
                int opcode = op % 100;
                int modes = op / 100;
                InstructionsSet[opcode](modes);
            }
        }

        private int ReadOperand(int index, int modes)
        {
            int mode = (modes / (int)(Math.Pow(10, index))) & 0x01;
            if (mode == 0)
            {
                // Position mode
                return Code[Code[Ip + index + 1]];
            }

            if (mode == 1)
            {
                // Immediate mode
                return Code[Ip + index + 1];
            }

            return 0;
        }

        private void WriteOperand(int index, int value)
        {
            // Always in position mode
            int x = Code[Ip + index + 1];
            Code[x] = value;                
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
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
                WriteOperand(2, a + b);
                Ip += 4;
            };

            // MUL command
            // Opcode 2 works exactly like opcode 1, except it multiplies the two inputs instead of adding them.
            // Again, the three integers after the opcode indicate where the inputs and outputs are, not their values.
            InstructionsSet[2] = (modes) =>
            {
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
                WriteOperand(2, a * b);
                Ip += 4;
            };

            // LOAD command
            // Opcode 3 takes a single integer as input and saves it to the position given by its only parameter.
            // For example, the instruction 3,50 would take an input value and store it at address 50.
            InstructionsSet[3] = (modes) =>
            {
                int input = Input.Dequeue();
                WriteOperand(0, input);
                Ip += 2;
            };

            // STORE command
            // Opcode 4 outputs the value of its only parameter.
            // For example, the instruction 4,50 would output the value at address 50.
            InstructionsSet[4] = (modes) =>
            {
                int a = ReadOperand(0, modes);
                Output.Enqueue(a);
                Ip += 2;
            };

            // JIT command
            // Opcode 5 is jump-if-true: if the first parameter is non-zero,
            // it sets the instruction pointer to the value from the second parameter. Otherwise, it does nothing.
            InstructionsSet[5] = (modes) =>
            {
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
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
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
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
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
                WriteOperand(2, a < b ? 1 : 0);
                Ip += 4;
            };

            // Opcode 8 is equals: if the first parameter is equal to the second parameter,
            // it stores 1 in the position given by the third parameter. Otherwise, it stores 0.
            InstructionsSet[8] = (modes) =>
            {
                int a = ReadOperand(0, modes);
                int b = ReadOperand(1, modes);
                WriteOperand(2, a == b ? 1 : 0);
                Ip += 4;
            };

            // Stop command
            InstructionsSet[99] = (mode) =>
            {
                Ip += 1;
                Running = false; 
            };
        }
    }   
}