using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Numbers
{
    public static class Gcd
    {
        public static ulong Calculate(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }
    }
}