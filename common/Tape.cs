using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Tape
    {
        private Dictionary<Int64, Int64> _tape;

        public Dictionary<Int64, Int64> Storage
        {
            get
            {
                return _tape;
            }
        }

        public Tape() 
        {
            _tape = new Dictionary<Int64, Int64>();            
        }

        public Int64 this[Int64 r]
        {
            get
            {
                if (!_tape.ContainsKey(r))
                {
                    _tape[r] = 0;
                }
                return _tape[r];
            }

            set
            {
                _tape[r] = value;
            }
        }
    }
}