using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common
{
    public enum CpuState
    {
        Running = 0,
        Waiting = 1,
        Exited = 2,
        InvalidInstruction = 3,
    }
}
