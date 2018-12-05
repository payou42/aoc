using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class GateWire : Gate
    {
        public string A { get; set; }

        public override void Activate(Registers<UInt16> r)
        {
            r[Output] = r[A];
        }

        public override string[] GetInputs()
        {
            return new string[] { A };
        }
    }
}
