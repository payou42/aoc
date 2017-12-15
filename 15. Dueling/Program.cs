using System;
using System.Text;
using System.Collections.Generic;

namespace _15._Dueling
{
    class Program
    {
        static Generator[] GENERATORS = null;

        static void CreateGenerators()
        {
            int[] source = Input.RAW;
            GENERATORS = new Generator[2];
            for (int i = 0; i < source.Length; ++i)
            {
                GENERATORS[i] = new Generator(source[i], Input.FACTORS[i], Input.DIVIDERS[i], Input.CHECKS[i]);
            }
        }

        static void Main(string[] args)
        {
            // Create the generators
            CreateGenerators();

            // Count matches
            int match = 0;
            for (int i = 0; i < Input.COUNT; ++i)
            {
                GENERATORS[0].Generate();
                GENERATORS[1].Generate();
                if (GENERATORS[0].GetLowWord() == GENERATORS[1].GetLowWord())
                {
                    match++;
                }
            }
            Console.WriteLine("Matches: {0}", match);
        }
    }
}
