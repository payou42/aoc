using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class Circuit
    {
        protected Registers<UInt16> _registers;

        protected List<Gate> _gates;

        public List<Gate> Gates
        {
            get
            {
                return _gates;
            }
        }

        public Registers<UInt16> Registers
        {
            get
            {
                return _registers;
            }
        }
       
        public Circuit()
        {
            _registers = new Registers<UInt16>();
            _gates = new List<Gate>();
        }

        public void Add(Gate gate)
        {
            _gates.Add(gate);
        }

        public void Run()
        {
            List<string> valid = new List<string>();
            List<Gate> toActivate = _gates.Select(g => g).ToList();
            
            while (toActivate.Count > 0)
            {
                List<Gate> activable = toActivate.Where(g => IsActivable(g, valid)).ToList();
                foreach (Gate gate in activable)
                {
                    gate.Activate(_registers);
                    valid.Add(gate.Output);
                    toActivate.Remove(gate);
                }
            }
        }

        private bool IsActivable(Gate g, List<string> valid)
        {
            foreach (string s in g.GetInputs())
            {
                if (!valid.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
