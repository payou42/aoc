using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201724 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tuple<int, int>[] _components;

        private int _max;
        
        private int _length;

        public Day201724()
        {
            Codename = "2017-24";
            Name = "Electromagnetic Moat";
        }

        public void Init()
        {
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            _components = new Tuple<int, int>[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                string[] c = input[i].Split("/");
                _components[i] = new Tuple<int, int>(Int32.Parse(c[0]), Int32.Parse(c[1]));
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            // Prepare data
            bool[] used = new bool[_components.Length];
            int[] chain = new int[_components.Length];
            int[] pin = new int[_components.Length];
            int[] steps = new int[_components.Length];
            int depth = 0;
            _max = 0;
            _length = 1;

            // Init data
            for (int i = 0; i < _components.Length; ++i)
            {
                used[i] = false;
                chain[i] = -1;
                pin[i] = 0;
                steps[i] = 0;
            }

            // Try all combinaison
            while (true)
            {
                // Find an item that match requirement for current depth
                bool found = false;
                for (int i = steps[depth]; i < _components.Length; ++i)
                {
                    if (used[i])
                        // Components already used
                        continue;

                    if ((_components[i].Item1 != pin[depth]) && (_components[i].Item2 != pin[depth]))
                        // Components does not match
                        continue;

                    // Try this component
                    found = true;
                    used[i] = true;
                    chain[depth] = i;
                    pin[depth + 1] = (_components[i].Item1 == pin[depth]) ? _components[i].Item2 : _components[i].Item1;
                    steps[depth] = i + 1;                    
                    depth++;
                    steps[depth] = 0;
                    break;
                }

                if (!found)
                {
                    // Check current solution
                    Evaluate(part, chain, depth - 1);

                    // Backtrack
                    depth--;
                    if (depth < 0)
                    {
                        // Process completed !
                        break;
                    }

                    // Revert counters
                    used[chain[depth]] = false;
                    chain[depth] = -1;
                }
            }
            
            // Finished !
            return _max.ToString();
        }

        private void Evaluate(Aoc.Framework.Part part, int[] chain, int len)
        {
            int strength = GetStrength(chain);

            if (part == Aoc.Framework.Part.Part1)
            {
                _max = Math.Max(strength, _max);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Check length first
                if (len > _length)
                {
                    _length = len;
                    _max = strength;
                }

                // Check strength
                if ((len == _length) && (strength > _max))
                {
                    _max = strength;
                }
            }
        }

        private int GetStrength(int[] chain)
        {
            int sum = 0;
            foreach (int i in chain)
            {
                if (i >= 0)
                {
                    sum += _components[i].Item1 + _components[i].Item2;
                }
            }
            return sum;
        }
    }
}