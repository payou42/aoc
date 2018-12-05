using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Logic
{
    public abstract class Gate
    {
        public string Output { get; set; }

        public abstract void Activate(Registers<UInt16> r);

        public abstract string[] GetInputs();
    }
}
