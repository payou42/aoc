using System;
using System.Text;
using System.Collections.Generic;

namespace _21._Fractal
{
    class RulesSet
    {  
        private Dictionary<string, string> _rules = null;

        public string this[string hash]
        {
            get
            {
                return _rules[hash];
            }
        }

        public RulesSet(string input)
        {
            _rules = new Dictionary<string, string>();
            string[] lines = input.Split("\r\n");
            foreach (string line in lines)
            {
                // Extract the hash & result
                string[] items = line.Split(" => ");
                string[] hash = items[0].Split("/");
                string result = String.Join("", items[1].Split("/"));

                // Console.WriteLine("Generating rules for hash: {0}", String.Join("", hash));
                AddAll(String.Join("", hash), result, hash.Length);
            }
            Console.WriteLine("");
        }
        
        private void Add(string hash, string result, string from)
        {
            if (!_rules.ContainsKey(hash))
            {
                _rules[hash] = result;
                // Console.WriteLine("\t{0} - {1}", hash, from);
            }
            else
            {
                if (_rules[hash] != result)
                {
                    Console.WriteLine("\t{0} - But hash already exists with different value!");
                }
            }
        }

        private void AddAll(string hash, string result, int length)
        {
            // Add original
            Add(hash, result, "Original");

            // Add rotated
            AddRotations(hash, result, length);

            // Add X-flipped
            string flippedx = FlipX(hash, length);
            Add(flippedx, result, "Flipped against X");
            AddRotations(flippedx, result, length);

            // Add Y-flipped
            string flippedy = FlipY(hash, length);
            Add(flippedy, result, "Flipped against Y");
            AddRotations(flippedy, result, length);
        }

        private void AddRotations(string hash, string result, int length)
        {
            string current = hash;
            for (int i = 0; i < 3; ++i)
            {
                current = Rotate(current, length);
                Add(current, result, String.Format("Rotated {1} time(s)", current, i + 1));
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
}