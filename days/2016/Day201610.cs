using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201610 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _target1 = 61;

        private int _target2 = 17;

        private string[] _input;

        private Dictionary<string, Bot> _bots;

        private Dictionary<string, (string, string)> _rules;

        public Day201610()
        {
            Codename = "2016-10";
            Name = "Balance Bots";
        }

        public void Init()
        {            
            _input = Aoc.Framework.Input.GetStringVector(this);
            _bots = new Dictionary<string, Bot>();
            _rules = new Dictionary<string, (string, string)>();            
            ParseRules();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                while (true)
                {
                    // Look for a bot that has 2 chips
                    Bot current = _bots.Values.FirstOrDefault(bot => bot.Count == 2);
                    if (current != null)
                    {
                        if (current.IsMatchingPair(_target1, _target2))
                        {
                            return current.Name;
                        }

                        // Apply rule
                        var rule = _rules[current.Name];
                        _bots[rule.Item1].Add(current.RemoveLowest());
                        _bots[rule.Item2].Add(current.RemoveFirst());
                    }
                    else
                    {
                        return "none";
                    }
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                while (true)
                {
                    // Look for a bot that has 2 chips
                    Bot current = _bots.Values.FirstOrDefault(bot => bot.Count == 2);
                    if (current != null)
                    {
                        // Apply rule
                        var rule = _rules[current.Name];
                        _bots[rule.Item1].Add(current.RemoveLowest());
                        _bots[rule.Item2].Add(current.RemoveFirst());
                    }
                    else
                    {
                        // End of the process
                        int o0 = _bots["output0"].RemoveFirst();
                        int o1 = _bots["output1"].RemoveFirst();
                        int o2 = _bots["output2"].RemoveFirst();
                        return (o0 * o1 * o2).ToString();
                    }
                }
            }

            return "";
        }

        private void ParseRules()
        {
            foreach (string rule in _input)
            {
                string[] words = rule.Split(" ");
                if (words[0] == "value")
                {
                    // Rule like:
                    // value 61 goes to bot 119
                    int value = int.Parse(words[1]);
                    string bot = words[4] + words[5];

                    // Grant the value to the correct bot
                    if (!_bots.ContainsKey(bot))
                    {
                        _bots[bot] = new Bot(bot);
                    }
                    _bots[bot].Add(value);
                }

                if (words[0] == "bot")
                {
                    // Rule like:
                    // bot 26 gives low to bot 131 and high to bot 149
                    string bot = words[0] + words[1];
                    string low = words[5] + words[6];
                    string high = words[10] + words[11];

                    // Create the bots
                    if (!_bots.ContainsKey(bot))
                    {
                        _bots[bot] = new Bot(bot);
                    }

                    if (!_bots.ContainsKey(low))
                    {
                        _bots[low] = new Bot(low);
                    }

                    if (!_bots.ContainsKey(high))
                    {
                        _bots[high] = new Bot(high);
                    }

                    // Create the rule
                    _rules.Add(bot, (low, high));
                }
            }
        }
    }   
}