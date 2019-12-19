using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    /// <summary>
    /// A dictionary-based registers system
    /// </summary>
    public class Registers<T>
    {
        private readonly Dictionary<string, T> _registers;

        public Dictionary<string, T> Storage
        {
            get
            {
                return _registers;
            }
        }

        public Registers() 
        {
            _registers = new Dictionary<string, T>();            
        }

        /// <summary>
        /// Get or set the content of a register
        /// </summary>
        /// <value>The content of the register, 0 by default</value>
        public T this[string r]
        {
            get
            {
                if (!_registers.ContainsKey(r))
                {
                    _registers[r] = default;
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
        public T GetLargest()
        {
            return _registers.Values.Max();
        }
    }
}