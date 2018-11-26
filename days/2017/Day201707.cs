using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201707 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tree _root;

        public Day201707()
        {
            Codename = "2017-07";
            Name = "Recursive Circus";
        }

        public void Init()
        {
            _root = BuildTree();
            _root.EvaluateWeight();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Evaluate weights
                return _root.Name;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Tuple<bool, string, int> balance = CheckBalanced(_root);
                return balance.Item3.ToString();
            }

            return "";
        }

        private Tree BuildTree()
        {
            // Create data
            Dictionary<String, Tree> elements = new Dictionary<String, Tree>();
            String[] lines = Aoc.Framework.Input.GetStringVector(this);

            // First build the list of elements
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");
                String name = items[0];
                Int32 weight = Int32.Parse(items[1].Substring(1, items[1].Length - 2));
                elements[name] = new Tree(name, weight);
            }

            // Then build dependencies
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");                
                if (items.Length > 3)
                {
                    Tree parent = elements[items[0]];
                    for (int j = 3; j < items.Length; ++j)
                    {
                        String childName = items[j];
                        if (childName[childName.Length - 1] == ',')
                        {
                            childName = childName.Substring(0, childName.Length - 1);
                        }
                        Tree child = elements[childName];
                        child.IsRoot = false;
                        parent.Children.Add(child);
                    }
                }
            }

            // Find the root
            Tree root = null;
            foreach(KeyValuePair<String, Tree> element in elements)
            {
                if (element.Value.IsRoot)
                {
                    root = element.Value;
                    break;
                }
            }

            return root;
        }

        private Tuple<bool, string, int> CheckBalanced(Tree tree)
        {
            // Check number of children
            if (tree.Children.Count == 0)
            {
                return new Tuple<bool, string, int>(true, "", 0);
            }

            // Check local balance
            Int32 w = tree.Children[0].TotalWeight;
            bool balanced = true;
            foreach (Tree child in tree.Children)
            {
                if (child.TotalWeight != w)
                {
                    balanced = false;
                }
            }
            if (balanced)
            {
                return new Tuple<bool, string, int>(true, "", 0);
            }

            // Check children balance
            foreach (Tree child in tree.Children)
            {
                Tuple<bool, string, int> result = CheckBalanced(child);
                if (!result.Item1)
                {
                    return result;
                }
            }

            // Local balance is wrong but children are ok
            Dictionary<int, int> weights = new Dictionary<int, int>();
            foreach (Tree child in tree.Children)
            {
                if (weights.ContainsKey(child.TotalWeight))
                {
                    weights[child.TotalWeight] += 1;
                }
                else 
                {
                    weights[child.TotalWeight] = 1;
                }
            }

            // Parse all the weights
            Int32 goodWeight = 0;
            Int32 wrongWeight = 0;
            foreach (KeyValuePair<int, int> subs in weights)
            {
                if (subs.Value == 1)
                {
                    wrongWeight = subs.Key;
                }
                else
                {
                    goodWeight = subs.Key;                        
                }
            }

            // Fix the tree !
            foreach (Tree child in tree.Children)
            {
                if (child.TotalWeight == wrongWeight)
                {
                    return new Tuple<bool, string, int>(false, child.Name, child.LocalWeight - wrongWeight + goodWeight);
                }
            }
            return new Tuple<bool, string, int>(true, "", 0);
        }
    }        
}