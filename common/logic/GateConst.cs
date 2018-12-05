using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class GateConst: Gate
    {
        public UInt16 A { get; set; }

        public override void Activate(Registers<UInt16> r)
        {
            r[Output] = A;
        }

        public override string[] GetInputs()
        {
            return new string[] { };
        }
    }
}
