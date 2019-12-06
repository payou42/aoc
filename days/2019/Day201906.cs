using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201906 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Tree<string> _orbits;

        private Tree<string>.Node _santa;

        private Tree<string>.Node _you;

        public Day201906()
        {
            Codename = "2019-06";
            Name = "Universal Orbit Map";
        }

        public void Init()
        {
            // Get the orbits description
            _input = Aoc.Framework.Input.GetStringVector(this);

            // Build the tree
            _orbits = new Tree<string>("COM");
            AddOrbits(_orbits.Root);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return CountOrbits(0, _orbits.Root).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Tree<string>.Node parent = _orbits.Root.CommonParent(_you, _santa);
                return (_you.DistanceToParent(parent) + _santa.DistanceToParent(parent) - 2).ToString();
            }

            return "";
        }

        private void AddOrbits(Tree<string>.Node parent)
        {
            foreach (string s in _input.Where(s => s.StartsWith(parent.Data)).Select(s => s.Substring(4)))
            {
                Tree<string>.Node child = parent.AddChild(s);
                if (s == "YOU")
                {
                    _you = child;
                }

                if (s == "SAN")
                {
                    _santa = child;
                }

                AddOrbits(child);
            }
        }

        private int CountOrbits(int distFromCom, Tree<string>.Node from)
        {
            int total = distFromCom;
            foreach (var child in from.Children)
            {
                total += CountOrbits(distFromCom + 1, child);
            }

            return total;
        }
    }   
}