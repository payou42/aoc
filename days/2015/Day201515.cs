using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201515 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Ingredient
        {
            public string Name { get; set; }

            public int[] Properties { get; set; }

            public int Calories { get; set; }
        }
        
        private Ingredient[] _ingredients;

        public Day201515()
        {
            Codename = "2015-15";
            Name = "Science for Hungry People";
        }

        public void Init()
        {
            _ingredients = Aoc.Framework.Input.GetStringMatrix(this, " ").Select(line => new Ingredient
            {
                Name = line[0].Substring(0, line[0].Length - 1),
                Properties = new int[4]
                {
                    int.Parse(line[2].Substring(0, line[2].Length - 1)),
                    int.Parse(line[4].Substring(0, line[4].Length - 1)),
                    int.Parse(line[6].Substring(0, line[6].Length - 1)),
                    int.Parse(line[8].Substring(0, line[8].Length - 1))
                },
                Calories = int.Parse(line[10])
            }).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Prepare data
                int totalCapacity = 100;
                Queue<List<int>> queue = new Queue<List<int>>();
                queue.Enqueue(new List<int>());

                // Try all combinaison
                long score = 0;
                while (queue.TryDequeue(out var recipie))
                {
                    if (recipie.Count == _ingredients.Length)
                    {
                        // Evaluate that recipie
                        score = Math.Max(score, EvaluateRecipie(recipie, false));
                    }
                    else
                    {
                        int usedCapacity = recipie.Sum();
                        for (int i = 0; i <= totalCapacity - usedCapacity; ++i)
                        {
                            List<int> next = recipie.Select(r => r).ToList();
                            next.Add(i);
                            queue.Enqueue(next);
                        }
                    }
                }

                return score.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Prepare data
                int totalCapacity = 100;
                Queue<List<int>> queue = new Queue<List<int>>();
                queue.Enqueue(new List<int>());

                // Try all combinaison
                long score = 0;
                while (queue.TryDequeue(out var recipie))
                {
                    if (recipie.Count == _ingredients.Length)
                    {
                        // Evaluate that recipie
                        score = Math.Max(score, EvaluateRecipie(recipie, true));
                    }
                    else
                    {
                        int usedCapacity = recipie.Sum();
                        for (int i = 0; i <= totalCapacity - usedCapacity; ++i)
                        {
                            List<int> next = recipie.Select(r => r).ToList();
                            next.Add(i);
                            queue.Enqueue(next);
                        }
                    }
                }

                return score.ToString();
            }

            return "";
        }

        private long EvaluateRecipie(List<int> recipie, bool checkCalorie)
        {
            if (checkCalorie)
            {
                long calories = 0;
                for (int j = 0; j < recipie.Count; ++j)
                {
                    calories += _ingredients[j].Calories * recipie[j];
                }
                if (calories != 500)
                {
                    return -1;
                }
            }
            
            long score = 1;
            for (int i = 0; i <_ingredients[0].Properties.Length; ++i)
            {
                long total = 0;
                for (int j = 0; j < recipie.Count; ++j)
                {
                    total += _ingredients[j].Properties[i] * recipie[j];
                }
                total = Math.Max(0, total);
                score *= total;
            }
            return score;
        }
    }   
}