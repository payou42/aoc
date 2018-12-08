using System;
using System.Text;
using System.Collections.Generic;
using Aoc.Common.Containers;

namespace Aoc.Common.Containers
{
    public class WeightTree
    {
        public class Header
        {
            public string Name { get; set; }

            public Int32 LocalWeight { get; set; }

            public Int32 TotalWeight { get; set; }
        }

        public Tree<Header> Tree { get; }

        public Tree<Header>.Node Root
        {
            get
            {
                return Tree.Root;
            }
        }

        public WeightTree(Header header)
        {
            Tree = new Tree<Header>(header);
        }

        public WeightTree(Tree<Header>.Node root)
        {
            Tree = new Tree<Header>(root);
        }
       
        public Int32 EvaluateWeight(Tree<Header>.Node node)
        {
            Int32 subWeights = 0;
            foreach (var child in node.Children)
            {
                subWeights += EvaluateWeight(child);
            }
            node.Data.TotalWeight = subWeights + node.Data.LocalWeight;
            return node.Data.TotalWeight;
        }
    }
}