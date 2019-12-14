using System;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Chemistry
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
}