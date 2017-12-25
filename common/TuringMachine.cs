using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc
{
    public class TuringMachine
    {
        private Tape _tape;

        private Int64 _cursor;

        private string _state;

        private Dictionary<string, Tuple<Int64, Direction, string>> _rules; 

        public TuringMachine(string initialState)
        {
            _tape = new Tape();
            _cursor = 0;
            _state = initialState;
            _rules = new Dictionary<string, Tuple<Int64, Direction, string>>();
        }

        public void AddRule(string state, Int64 write, Direction move, string next)
        {
            _rules[state] = new Tuple<Int64, Direction, string>(write, move, next);
        }
        
        public void Step()
        {
            Tuple<Int64, Direction, string> rule = _rules[_state + "_" + _tape[_cursor].ToString()];
            _tape[_cursor] = rule.Item1;
            _cursor += (rule.Item2 == Direction.Right) ? 1 : -1;
            _state = rule.Item3;
        }

        public Int64 CountOnes()
        {
            Int64 ones = 0;
            foreach (Int64 v in _tape.Storage.Values)
            {
                ones += v;
            }
            return ones;
        }
    }

}