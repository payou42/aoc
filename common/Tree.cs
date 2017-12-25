using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc
{
    public class Tree
    {
        public string Name { get; set; }

        public Int32 LocalWeight { get; set; }

        public Int32 TotalWeight { get; set; }

        public List<Tree> Children { get; }

        public Tree Parent { get; }

        public bool IsRoot { get; set; }

        public Tree(string name, int weight, Tree parent = null)
        {
            Name = name;
            LocalWeight = weight;
            Children = new List<Tree>();
            IsRoot = true;
            Parent = parent;
        }

        public Tree AddChild(string name, int weight)
        {
            Tree child = new Tree(name, weight, this);
            Children.Add(child);
            return child;
        }
        
        public Int32 EvaluateWeight()
        {
            Int32 subWeights = 0;
            foreach (Tree element in Children)
            {
                subWeights += element.EvaluateWeight();                
            }
            TotalWeight = subWeights + LocalWeight;
            return TotalWeight;
        }        
    }
}