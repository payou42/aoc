using System;
using System.Text;
using System.Collections.Generic;

namespace _7._Circus
{
    class Element
    {
        public string Name { get; set; }

        public Int32 Weight { get; set; }

        public Int32 TowerWeight { get; set; }

        public List<Element> Children { get; }

        public bool IsRoot { get; set; }

        public Element(string name, int weight) {
            Name = name;
            Weight = weight;
            Children = new List<Element>();
            IsRoot = true;
        }

        public Int32 EvaluateWeight()
        {
            Int32 subWeights = 0;
            foreach (Element element in Children)
            {
                subWeights += element.EvaluateWeight();                
            }
            TowerWeight = subWeights + Weight;
            return TowerWeight;
        }

        public bool CheckBalanced()
        {
            if (Children.Count == 0)
            {
                return true;
            }

            Int32 w = Children[0].TowerWeight;
            bool balanced = true;
            bool result = true;
            foreach (Element child in Children)
            {
                if (child.TowerWeight != w)
                {
                    balanced = false;
                    result = false;
                }
            }

            if (!balanced)
            {
                foreach (Element child in Children)
                {
                    if (!child.CheckBalanced())
                    {
                        Console.WriteLine("{0} is unbalanced due to {1}", Name, child.Name);
                        balanced = true; // This is not this element's fault
                    }
                }
            }

            if (!balanced)
            {
                // Build the list of weights
                Dictionary<int, int> weights = new Dictionary<int, int>();
                foreach (Element child in Children)
                {
                    if (weights.ContainsKey(child.TowerWeight))
                    {
                        weights[child.TowerWeight] += 1;
                    }
                    else 
                    {
                        weights[child.TowerWeight] = 1;
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
                foreach (Element child in Children)
                {
                   if (child.TowerWeight == wrongWeight)
                   {
                       Console.WriteLine("{0} is unbalanced due to {1}. {1} total weight should be {2} instead of {3}", Name, child.Name, goodWeight, wrongWeight);
                       Console.WriteLine(" -> Fix it by changing {0}'s weight from {1} to {2}", child.Name, child.Weight, child.Weight - wrongWeight + goodWeight);
                   }
                }

            }
            return result;
        }
    }
}