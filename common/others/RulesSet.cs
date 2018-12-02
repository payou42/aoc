using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common
{    
    public class RulesSet
    {  
        private Dictionary<string, string> _rules = null;

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
}