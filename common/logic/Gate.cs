using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public abstract class Gate
    {
        public string Output { get; set; }

        public string[] Inputs { get; set; }

        public abstract void Tick(Registers<UInt16> r);

        public string[] GetDependencies()
        {
            return Inputs.Where(s => !UInt16.TryParse(s, out var n)).ToArray();
        }

        protected UInt16 GetInput(int i, Registers<UInt16> r)
        {
            if (UInt16.TryParse(Inputs[i], out var n))
            {
                return n;
            }
            return r[Inputs[i]];
        }
    }
}
