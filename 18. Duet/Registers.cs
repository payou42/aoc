using System;
using System.Text;
using System.Collections.Generic;

namespace _18._Duet
{
    class Registers
    {
        private Dictionary<string, Int64> _registers;

        public Dictionary<string, Int64> Storage
        {
            get
            {
                return _registers;
            }
        }

        public Registers() 
        {
            _registers = new Dictionary<string, Int64>();            
        }

        public Int64 this[string r]
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
    }
}