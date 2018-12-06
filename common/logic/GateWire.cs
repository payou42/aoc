using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class GateWire : Gate
    {
        public override void Tick(Registers<UInt16> r)
        {
            r[Output] = GetInput(0, r);
        }
    }
}
