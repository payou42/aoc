using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common
{
    public class Scanner
    {
        public int Range { get; }

        public int Depth { get; }

        public int Severity
        {
            get
            {
                return Depth * Range;
            }
        }

        public Scanner(int depth, int range)
        {
            Depth = depth;
            Range = range;
        }

        public bool IsScanned(int start)
        {
            return (start + Depth) % (2 * Range - 2) == 0;
        }
    }
}
