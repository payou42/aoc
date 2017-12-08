using System;
using System.Text;
using System.Collections.Generic;

namespace _8._Registers
{
    class Registers
    {
        private Dictionary<string, int> _registers;

        public Registers() 
        {
            _registers = new Dictionary<string, int>();
        }

        public int this[string r]
        {
            get
            {
                if (!_registers.ContainsKey(r))
                {
                    _registers[r] = 0;
                }
                return _registers[r];
            }

            set
            {
                _registers[r] = value;
            }
        }

        public int GetLargest()
        {
            int largest = Int32.MinValue;
            foreach(KeyValuePair<string, int> r in _registers)
            {
                largest = Math.Max(largest, r.Value);
            }
            return largest;
        }
    }
}