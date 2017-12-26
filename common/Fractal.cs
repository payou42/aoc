using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common
{
    public class Fractal    
    {
        private string[] _fractal;

        public static string Initial = @".#.,..#,###";

        public Fractal()
        {
            _fractal = Initial.Split(",");
        }

        public void Iterate(RulesSet rules)
        {
            // Get the step
            int steps = 3;
            if (_fractal.Length % 2 == 0)
                steps = 2;

            // Prepare the new fractal at the correct size
            string[] after = new string[(steps + 1) * _fractal.Length / steps];
            for (int i = 0; i < (steps + 1) * _fractal.Length / steps; ++i)
            {
                after[i] = "";
            }

            // Process it in column
            for (int i = 0; i < _fractal.Length / steps; ++i)
            {
                for (int j = 0; j < _fractal.Length / steps; ++j)
                {                    
                    // Extract the hash
                    string hash = "";
                    for (int k = 0; k < steps; ++k)
                    {
                        hash += _fractal[(j * steps) + k].Substring(i * steps, steps);
                    }

                    // Get the matchine rule in the rules set
                    string rule = rules[hash];

                    // Apply rule
                    for (int k = 0; k < steps + 1; ++k)
                    {
                        after[j * (steps + 1) + k] += rule.Substring(k * (steps + 1), steps + 1);
                    }
                }
            }

            // All done
            _fractal = after;
        }

        public int CountPixels()
        {
            int counter = 0;
            foreach (string line in _fractal)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '#')
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}