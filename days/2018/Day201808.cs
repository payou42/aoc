using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201808 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }
        
        public class Header
        {
            public int[] Metadata { get; set; }

            public long Sum
            {
                get
                {
                    return (Metadata != null) ? Metadata.Sum() : 0;
                }
            }
        }

        private int[] _input;

        private Tree<Header> _tree;

        public Day201808()
        {
            Codename = "2018-08";
            Name = "Memory Maneuver";
        }

        public void Init()
        {
            // Get the input
            _input = Aoc.Framework.Input.GetIntVector(this, " ");

            // Build the tree
            _tree = new Tree<Header>(new Header());
            ParseNode(_tree.Root, 0);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long sum = 0;
                _tree.Root.Traverse((h) => sum += h.Sum);
                return sum.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Dictionary<Tree<Header>.Node, int> cache = new Dictionary<Tree<Header>.Node, int>();
                CalculateSum(_tree.Root, cache);
                return cache[_tree.Root].ToString();
            }

            return "";
        }

        private void CalculateSum(Tree<Header>.Node node, Dictionary<Tree<Header>.Node, int> cache)
        {
            if (cache.ContainsKey(node))
            {
                return;
            }

            int nChild = node.Children.Count;
            int sum = 0;
            if (nChild > 0)
            {
                foreach (int n in node.Data.Metadata)
                {
                    if (n >= 1 && n <= nChild)
                    {
                        Tree<Header>.Node child = node.Children[n - 1];
                        CalculateSum(child, cache);
                        sum += cache[child];
                    }
                }
            }
            else
            {
                sum = (int)node.Data.Sum;
            }
            cache[node] = sum;
        }

        private int ParseNode(Tree<Header>.Node node, int index)
        {
            // Count the children
            int current = index;
            int children = _input[current++];
            int metalength = _input[current++];                

            // Add the children
            for (int j = 0; j < children; ++j)
            {
                Tree<Header>.Node child  = node.AddChild(new Header());
                current = ParseNode(child, current);                
            }

            // Set the metadata of the current node
            node.Data.Metadata = _input.Skip(current).Take(metalength).ToArray();
            current += metalength;
            return current;
        }
    }   
}