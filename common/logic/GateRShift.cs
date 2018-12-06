using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public class GateRShift : Gate
    {
        public override void Tick(Registers<UInt16> r)
        {
            r[Output] = (UInt16)(GetInput(0, r) >> GetInput(1, r));
        }
    }
}
