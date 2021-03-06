using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201914 : Aoc.Framework.IDay
    {
        public class Reaction
        {
            public Reaction()
            {
                Elements = new Dictionary<string, long>();
            }

            public Dictionary<string, long> Elements { get; }

            public static Reaction Parse(string s)
            {
                // Create a new reaction
                Reaction reaction = new Reaction();

                // Parse a string like 
                // 1 FDWSG, 1 VCWNW, 4 BDKN, 14 FDVNC, 1 CLZT, 62 SGLGN, 5 QMJK, 26 ZDGPL, 60 KCMH, 32 FVQM, 15 SRWZ => 1 FUEL
                string[] sides = s.Split("=>");
                string[] consumed = sides[0].Trim().Split(",");
                string[] produced = sides[1].Trim().Split(",");

                foreach (string c in consumed)
                {
                    string[] parts = c.Trim().Split(" ");
                    reaction.Elements[parts[1]] = -int.Parse(parts[0]);
                }

                foreach (string p in produced)
                {
                    string[] parts = p.Trim().Split(" ");
                    reaction.Elements[parts[1]] = int.Parse(parts[0]);
                }

                return reaction;
            }

            public bool Produce(string s)
            {
                return Elements.ContainsKey(s) && Elements[s] > 0;
            }

            public void Apply(Dictionary<string, long> components, long count)
            {
                foreach (var kv in Elements)
                {
                    if (!components.ContainsKey(kv.Key))
                        components[kv.Key] = 0;

                    components[kv.Key] += count * kv.Value;
                }
            }
        }
        
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Reaction[] _reactions;

        private Dictionary<string, long> _components;

        private long _min;

        public Day201914()
        {
            Codename = "2019-14";
            Name = "Space Stoichiometry";
        }

        public void Init()
        {
            // Read available reactions
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            _reactions = new Reaction[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                _reactions[i] = Reaction.Parse(input[i]); 
            }
            
            // Prepare data
            _components = new Dictionary<string, long>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _components.Clear();
                _components["FUEL"] = -1;
                _min = ApplyReactions();
                return _min.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long target = 1000000000000;
                long start = target / _min;
                long stop = start * 2;

                while (start != stop)
                {
                    // Dichotomic search
                    long current = (start + stop) / 2;
                    _components.Clear();
                    _components["FUEL"] = -current;
                    long ore = ApplyReactions();
                    if (ore > target)
                    {
                        // Too much fuel requested
                        stop = current - 1;
                    }
                    else
                    {
                        start = current; 
                    }
                }

                return start.ToString();
            }

            return "";
        }

        private long ApplyReactions()
        {
            long minOre = long.MaxValue;
            Queue<Dictionary<string, long>> states = new Queue<Dictionary<string, long>>();
            states.Enqueue(_components);

            while (states.Count > 0)
            {
                // Get the state to check
                var state = states.Dequeue();

                // Apply reaction when we have no choice
                Reduce(state);
                
                // Check if it's a final state
                if ((state.Count(kv => kv.Value < 0) == 1) && (state.ContainsKey("ORE") && (state["ORE"] < 0)))
                {
                    minOre = Math.Min(-state["ORE"], minOre);
                    continue;
                }

                // Look for missing elements
                // This is actually never used, my cpu thanks the puzzle creator for that :)
                foreach (string s in state.Where(kv => kv.Value < 0).Select(kv => kv.Key))
                {
                    // Get the list of reaction that can produce that element
                    var available = _reactions.Where(r => r.Produce(s));

                    foreach (var r in available)
                    {
                        var newState = new  Dictionary<string, long>(state);
                        r.Apply(newState, 1);
                        states.Enqueue(newState);
                    }
                }
            }

            return minOre;
        }

        private void Reduce(Dictionary<string, long> state)
        {
            // Have we done something ?
            bool reduced = true;

            while (reduced)
            {
                reduced = false;
                foreach (string e in state.Where(kv => kv.Value < 0).Select(kv => kv.Key))
                {
                    // Get the list of reaction that can produce that element
                    var available = _reactions.Where(r => r.Produce(e));

                    // If there's only one, reduce the reaction
                    if (available.Count() == 1)
                    {
                        Reaction r = available.First();
                        long count = (long)Math.Ceiling(-(double)state[e] / (double)r.Elements[e]);
                        r.Apply(state, count);
                        reduced = true; 
                        break;                
                    }
                }
            }
        }
    }   
}