using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202007 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Dictionary<string, Dictionary<string, int>> _rules;

        public Day202007()
        {
            Codename = "2020-07";
            Name = "Handy Haversacks";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
            _rules = new Dictionary<string, Dictionary<string, int>>();
            ParseRules();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Look for all bags that can contains a shiny bag
                HashSet<string> container = new HashSet<string>();
                container.Add("shinygold");
                int len = 0;

                while (len != container.Count)
                {
                    len = container.Count;
                    foreach (var rule in _rules)
                    {
                        foreach (var entry in rule.Value)
                        {
                            if (container.Contains(entry.Key))
                            {
                                container.Add(rule.Key);
                            }
                        }
                    }
                }

                // -1 in order to remove shiny bag from the possible containers
                return (container.Count - 1).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Dictionary<string, long> allbags = new Dictionary<string, long>();
                while (allbags.Count != _rules.Count)
                {
                    foreach (var rule in _rules.Where(kvp => !allbags.ContainsKey(kvp.Key) && kvp.Value.Keys.All(b => allbags.ContainsKey(b))))
                    {
                        allbags[rule.Key] = 1 + rule.Value.Sum(kvp => kvp.Value * allbags[kvp.Key]);
                    }
                }

                return (allbags["shinygold"] - 1).ToString();
            }

            return "";
        }

        private void ParseRules()
        {
            foreach (string line in _input)
            {
                // mirrored green bags contain 3 mirrored violet bags, 2 faded beige bags.
                string[] items = line.Split(" ");
                string bag = items[0] + items[1];
                _rules[bag] = new Dictionary<string, int>();

                int contentIndex = 4;
                while (contentIndex < items.Length)
                {
                    string b = items[contentIndex + 1] + items[contentIndex + 2];
                    if (int.TryParse(items[contentIndex], out int q))
                    {
                        _rules[bag][b] = q;
                    }

                    contentIndex += 4;
                }
            }
        }
    }   
}