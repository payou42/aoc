using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201523 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201523()
        {
            Codename = "2015-23";
            Name = "Opening the Turing Lock";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Fast method
                int b = Process(4591);
                return b.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int b = Process(113383);
                return b.ToString();
            }

            return "";
        }

        private int Process(int input)
        {
            int a = input;
            int b = 0;
            while (a != 1)
            {
                b++;
                if (a % 2 == 0)
                {
                    a /= 2;
                }
                else
                {
                    // Optimisation : instead of just setting a = 3a + 1 that can grow large, we use the fact that a is odd
                    // So:
                    //    a = 3a + 1 with a = 2n + 1 (odd number)
                    //    a = 3(2n+1) + 1
                    //    a = 6n + 4, which is even, so we can directly apply first rule
                    //    a = 3n + 2, b = b + 1, with n= 2/a, rounded down
                    b++;
                    a = 3 * (a / 2) + 2;
                }
            }

            return b;
        }
    }   
}