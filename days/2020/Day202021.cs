using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202021 : Aoc.Framework.IDay
    {
        public class Recipie
        {        
            public List<string> Ingredients;

            public List<string> Allergens;

            public Recipie(string input)
            {
                var items = input.Split("(");
                this.Ingredients = items[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                this.Allergens = items.Length == 1 ? new List<string>() : items[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s[..^1]).Skip(1).ToList();
            }
        }

        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Recipie[] _recipies;

        private Dictionary<string, string> _mapping;

        public Day202021()
        {
            Codename = "2020-21";
            Name = "Allergen Assessment";
        }

        public void Init()
        {
            _recipies = Aoc.Framework.Input.GetStringVector(this).Select(line => new Recipie(line)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Build the list of recipies containing a given allergens
                Dictionary<string, List<string>> matches = new Dictionary<string, List<string>>();
                foreach (Recipie r in _recipies)
                {
                    foreach (string a in r.Allergens)
                    {
                        if (!matches.ContainsKey(a))
                            matches[a] = r.Ingredients;
                        else
                            matches[a] = matches[a].Intersect(r.Ingredients).ToList();
                    }
                }

                // Solve allergens
                _mapping = new Dictionary<string, string>();
                while (_mapping.Count != matches.Count)
                {
                    var solvable = matches.Where(kvp => kvp.Value.Count == 1).ToList();
                    foreach (var s in solvable)
                    {
                        string ingredient = s.Value.First();
                        _mapping[s.Key] = ingredient;
                        foreach (var p in matches)
                            p.Value.Remove(ingredient);
                    }
                }

                // Answer
                return _recipies.Sum(r => r.Ingredients.Count(i => !_mapping.Values.Contains(i))).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // We already solved part 2 in part 1
                // There's probably a faster way for part 1, like just counting the number of allergens ?
                return string.Join(",", _mapping.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value));
            }

            return "";
        }
    }   
}