using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    /// <summary>
    /// A dictionary-based registers system
    /// </summary>
    public class Registers
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

        /// <summary>
        /// Get or set the content of a register
        /// </summary>
        /// <value>The content of the register, 0 by default</value>
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

        /// <summary>
        /// Get the maximum value from all registers
        /// </summary>
        /// <returns>The maximum</returns>
        public Int64 GetLargest()
        {
            return _registers.Values.Max();
        }
    }
}