using System;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc.Common.Numbers
{
    public static class Lcm
    {
        public static ulong Calculate(ulong a, ulong b)
        {
            ulong num1, num2;
            if (a > b)
            {
                num1 = a; num2 = b;
            }
            else
            {
                num1 = b; num2 = a;
            }

            for (ulong i = 1; i < num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num1 * num2;
        }
    }
}
