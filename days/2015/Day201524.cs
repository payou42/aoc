using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201524 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _gifts;

        public Day201524()
        {
            Codename = "2015-24";
            Name = "It Hangs in the Balance";
        }

        public void Init()
        {
            _gifts = Aoc.Framework.Input.GetIntVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return BestEntanglement(3).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return BestEntanglement(4).ToString();
            }

            return "";
        }

        private long BestEntanglement(int nbGroups)
        {
            // We need to divide the weights equally
            int targetWeight = _gifts.Sum() / nbGroups;

            // Find the minimum number of gifts to put in front
            int bestCount = int.MaxValue;
            long bestEntanglement = int.MaxValue;

            // Prepare the breadth-first search
            Queue<(List<int>, List<int>)> queue = new Queue<(List<int>, List<int>)>();
            queue.Enqueue((new List<int>(), _gifts.Where(w => w <= targetWeight).OrderByDescending(w => w).ToList()));

            // Search for the best setup
            while (queue.TryDequeue(out var state))
            {
                if (state.Item1.Sum() == targetWeight)
                {
                    if (bestCount > state.Item1.Count)
                    {
                        bestCount = state.Item1.Count;
                        bestEntanglement = state.Item1.Aggregate((long)1, (acc, v) => (long)acc * (long)v);
                    }
                    else if (bestCount == state.Item1.Count)
                    {
                        bestEntanglement = Math.Min(bestEntanglement, state.Item1.Aggregate((long)1, (acc, v) => (long)acc * (long)v));
                    }
                    continue;
                }

                if (state.Item1.Count >= bestCount)
                {
                    // Prune this path
                    continue;
                }

                // Enqueue possibilities
                int max = state.Item1.Count > 0 ? state.Item1.Last() : int.MaxValue;
                foreach (int w in state.Item2.Where(g => g < max))
                {
                    queue.Enqueue((state.Item1.Append(w).ToList(), state.Item2.Where(g => g < w).ToList()));
                }
            }
            return bestEntanglement;
        }
    }   
}