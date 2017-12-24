using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc
{
    class Generator
    {
        public int Value { get; private set; }

        public int Factor { get; }

        public int Modulo { get; }

        public int Check { get; }
        
        public Generator(int seed, int factor, int modulo, int check)
        {
            Value = seed;
            Factor = factor;
            Modulo = modulo;
            Check = check;
        }

        private void Next()
        {
            Int64 current = (Int64)Value;
            current = (current * Factor) % Modulo;
            Value = (Int32)current;
        }

        public int Generate()
        {
            for (;;)
            {
                Next();
                if ((Check <= 0) || (Value % Check == 0))
                {
                    break;
                }
            }
            return Value;
        }

        public Int16 GetLowWord()
        {
            return (Int16)(Value & 0xFFFF);
        }
    }
}