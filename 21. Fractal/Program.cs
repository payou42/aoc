using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _21._Fractal
{
    class Program
    {
        static void Main(string[] args)
        {            
            // Load the rules
            RulesSet rules = new RulesSet(Input.RULES_RAW);
            
            // Create the fractal
            Fractal fractal = new Fractal(Input.START);

            // Run a few iteration
            for (int i = 0; i < Input.COUNT_RAW; ++i)
            {
                fractal.Iterate(rules);
            }

            // Count the pixels
            Console.WriteLine("Number of pixels: {0}", fractal.CountPixels());
        }
    }
}
