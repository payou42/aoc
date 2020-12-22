using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Strings;

namespace Aoc
{
    public class Day201519 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<string, List<string>> _reactions;

        private Dictionary<string, List<string>> _reductions;

        private string _molecule;

        public Day201519()
        {
            Codename = "2015-19";
            Name = "Medicine for Rudolph";
        }

        public void Init()
        {
            _reactions = new Dictionary<string, List<string>>();
            _reductions = new Dictionary<string, List<string>>();
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            _molecule = input[^1];
            for (int i = 0; i < input.Length - 1; ++i)
            {
                string[] line = input[i].Split(" ");
                if (!_reactions.ContainsKey(line[0]))
                {
                    _reactions[line[0]] = new List<string>();
                }
                if (!_reductions.ContainsKey(line[2]))
                {
                    _reductions[line[2]] = new List<string>();
                }

                _reactions[line[0]].Add(line[2]);
                _reductions[line[2]].Add(line[0]);
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return GetReactions(_molecule, _reactions).Count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Each reaction adds exactly ONE molecule (from 1 to 2)
                // Except from :
                // - Reactions that produces Rn, they add 2 molecules
                // - Reactions that produces Y, they add 2 more molecules
                int elements = _molecule.Where(c => char.IsUpper(c)).Count();
                int rn = Repetition.Count(_molecule, "Rn");
                int y = Repetition.Count(_molecule, "Y");
                return (elements - rn*2 - y*2 - 1).ToString();
            }

            return "";
        }

        private HashSet<string> GetReactions(string input, Dictionary<string, List<string>> reactions)
        {
            HashSet<string> molecules = new HashSet<string>();
            foreach (var reaction in reactions)
            {
                for (int i = 0; i <= input.Length - reaction.Key.Length; ++i)
                {
                    if (input.Substring(i, reaction.Key.Length) == reaction.Key)
                    {
                        // Build the new molecule
                        foreach (string result in reaction.Value)
                        {
                            molecules.Add(input.Splice(i, reaction.Key.Length, result));
                        }
                    }
                }
            }
            return molecules;
        }
    }   
}