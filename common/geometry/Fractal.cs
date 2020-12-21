using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common.Geometry
{
    public class Fractal    
    {
        public class RulesSet
        {  
            private readonly Dictionary<string, string> _rules = null;

            public string this[string hash]
            {
                get
                {
                    return _rules[hash];
                }
            }

            public RulesSet(string[] rules)
            {
                _rules = new Dictionary<string, string>();
                foreach (string rule in rules)
                {
                    // Extract the hash & result
                    string[] items = rule.Split(" => ");
                    string[] hash = items[0].Split("/");
                    string result = String.Join("", items[1].Split("/"));

                    AddAll(String.Join("", hash), result, hash.Length);
                }
            }
            
            private void Add(string hash, string result)
            {
                _rules[hash] = result;
            }

            private void AddAll(string hash, string result, int length)
            {
                // Add original
                Add(hash, result);

                // Add rotated
                AddRotations(hash, result, length);

                // Add X-flipped
                string flippedx = FlipX(hash, length);
                Add(flippedx, result);
                AddRotations(flippedx, result, length);

                // Add Y-flipped
                string flippedy = FlipY(hash, length);
                Add(flippedy, result);
                AddRotations(flippedy, result, length);
            }

            private void AddRotations(string hash, string result, int length)
            {
                string current = hash;
                for (int i = 0; i < 3; ++i)
                {
                    current = Rotate(current, length);
                    Add(current, result);
                }
            }

            private string Rotate(string hash, int length)
            {
                string rotated = "";
                for (int j = 0; j < length; ++j)
                {
                    for (int i = 0; i < length; ++i)
                    {
                        rotated += hash[length - j - 1 + i * length];
                    }
                }
                return rotated;
            }

            public string FlipX(string hash, int length)
            {
                string flipped = "";
                for (int j = 0; j < length; ++j)
                {
                    for (int i = 0; i < length; ++i)
                    {
                        flipped += hash[length - i - 1 + j * length];
                    }
                }
                return flipped;
            }

            public string FlipY(string hash, int length)
            {
                string flipped = "";
                for (int j = 0; j < length; ++j)
                {
                    for (int i = 0; i < length; ++i)
                    {
                        flipped += hash[i + (length - j - 1) * length];
                    }
                }
                return flipped;
            }
        }
        
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