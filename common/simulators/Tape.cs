using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Simulators
{
    /// <summary>
    /// An object representing a linear tape, used in Turing machines
    /// </summary>
    public class Tape
    {
        private Dictionary<Int64, Int64> _tape;

        private Int64 _cursor;


        public Dictionary<Int64, Int64> Storage
        {
            get
            {
                return _tape;
            }
        }

        public Int64 Cursor
        {
            get
            {
                return _cursor;
            }

            set
            {
                _cursor = value;
            }
        }

        public Tape() 
        {
            _tape = new Dictionary<Int64, Int64>();
            _cursor = 0;
        }

        /// <summary>
        /// Get or set the value at the current cursor of the tape (plus an offset)
        /// </summary>
        /// <value>The value of the tape at the position</value>
        public Int64 this[Int64 r]
        {
            get
            {
                if (!_tape.ContainsKey(_cursor + r))
                {
                    _tape[_cursor + r] = 0;
                }
                return _tape[_cursor + r];
            }

            set
            {
                _tape[_cursor + r] = value;
            }
        }        
    }
}