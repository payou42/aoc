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

        public List<Gate> Gates { get; set; }

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
            Gates = new List<Gate>();
        }

        public void Tick()
        {
            List<string> valid = new List<string>();
            List<Gate> toActivate = Gates.Select(g => g).ToList();
            
            while (toActivate.Count > 0)
            {
                List<Gate> activable = toActivate.Where(g => IsActivable(g, valid)).ToList();
                foreach (Gate gate in activable)
                {
                    gate.Tick(_registers);
                    valid.Add(gate.Output);
                    toActivate.Remove(gate);
                }
            }
        }

        public void Reset()
        {
            _registers = new Registers<UInt16>();
        }

        private bool IsActivable(Gate g, List<string> valid)
        {
            foreach (string s in g.GetDependencies())
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
