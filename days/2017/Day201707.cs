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

        private WeightTree _tree;

        public Day201707()
        {
            Codename = "2017-07";
            Name = "Recursive Circus";
        }

        public void Init()
        {
            _tree = BuildTree();
            _tree.EvaluateWeight(_tree.Root);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Evaluate weights
                return _tree.Root.Data.Name;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Tuple<bool, string, int> balance = CheckBalanced(_tree.Root);
                return balance.Item3.ToString();
            }

            return "";
        }

        private WeightTree BuildTree()
        {
            // Create data
            Dictionary<String, Tree<WeightTree.Header>.Node> elements = new Dictionary<String, Tree<WeightTree.Header>.Node>();
            String[] lines = Aoc.Framework.Input.GetStringVector(this);

            // First build the list of elements
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");
                String name = items[0];
                Int32 weight = Int32.Parse(items[1].Substring(1, items[1].Length - 2));
                elements[name] = new Tree<WeightTree.Header>.Node(null, new WeightTree.Header { Name = name, LocalWeight = weight });
            }

            // Then build dependencies
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");                
                if (items.Length > 3)
                {
                    var parent = elements[items[0]];
                    for (int j = 3; j < items.Length; ++j)
                    {
                        String childName = items[j];
                        if (childName[childName.Length - 1] == ',')
                        {
                            childName = childName.Substring(0, childName.Length - 1);
                        }
                        var child = elements[childName];
                        parent.Children.Add(child);
                        child.Parent = parent;
                    }
                }
            }

            // Find the root
            return new WeightTree(elements.Values.Where(n => n.IsRoot).First());
        }

        private Tuple<bool, string, int> CheckBalanced(Tree<WeightTree.Header>.Node tree)
        {
            // Check number of children
            if (tree.Children.Count == 0)
            {
                return new Tuple<bool, string, int>(true, "", 0);
            }

            // Check local balance
            Int32 w = tree.Children[0].Data.TotalWeight;
            bool balanced = true;
            foreach (var child in tree.Children)
            {
                if (child.Data.TotalWeight != w)
                {
                    balanced = false;
                }
            }
            if (balanced)
            {
                return new Tuple<bool, string, int>(true, "", 0);
            }

            // Check children balance
            foreach (var child in tree.Children)
            {
                Tuple<bool, string, int> result = CheckBalanced(child);
                if (!result.Item1)
                {
                    return result;
                }
            }

            // Local balance is wrong but children are ok
            Dictionary<int, int> weights = new Dictionary<int, int>();
            foreach (var child in tree.Children)
            {
                if (weights.ContainsKey(child.Data.TotalWeight))
                {
                    weights[child.Data.TotalWeight] += 1;
                }
                else 
                {
                    weights[child.Data.TotalWeight] = 1;
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
            foreach (var child in tree.Children)
            {
                if (child.Data.TotalWeight == wrongWeight)
                {
                    return new Tuple<bool, string, int>(false, child.Data.Name, child.Data.LocalWeight - wrongWeight + goodWeight);
                }
            }
            return new Tuple<bool, string, int>(true, "", 0);
        }
    }        
}