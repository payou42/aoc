using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201517 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _capacities;

        public Day201517()
        {
            Codename = "2015-17";
            Name = "No Such Thing as Too Much";
        }

        public void Init()
        {
            _capacities = Aoc.Framework.Input.GetIntVector(this);
            Array.Sort(_capacities);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Solution counter
                long count = 0;

                // Initial data
                Queue<(List<int>, List<int>)> queue = new Queue<(List<int>, List<int>)>();
                queue.Enqueue((new List<int>(), _capacities.ToList()));
                
                // Test all possibilities
                while (queue.TryDequeue(out var current))
                {
                    // Check current solution
                    int liters = current.Item1.Sum();
                    if (liters >= 150)
                    {          
                        if (liters == 150)
                        {
                            count++;
                        }
                        continue;
                    }
                    
                    // Enqueue all other possibilities
                    for (int i = 0; i < current.Item2.Count; ++i)
                    {
                        var next = current.Item1.Select(o => o).ToList();
                        next.Add(current.Item2[i]);
                        var remaining = current.Item2.Skip(i + 1).ToList();
                        queue.Enqueue((next, remaining));
                    }
                }

                // All done
                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Solution counter
                long count = 0;
                long used = long.MaxValue;

                // Initial data
                Queue<(List<int>, List<int>)> queue = new Queue<(List<int>, List<int>)>();
                queue.Enqueue((new List<int>(), _capacities.ToList()));
                
                // Test all possibilities
                while (queue.TryDequeue(out var current))
                {
                    // Discard too long solution
                    int l = current.Item1.Count;
                    if (l > used)
                    {
                        continue;
                    }

                    // Check current solution
                    int liters = current.Item1.Sum();
                    if (liters >= 150)
                    {          
                        if (liters == 150)
                        {
                            if (l < used)
                            {
                                used = l;
                                count = 0;
                            }
                            count++;
                        }
                        continue;
                    }
                    
                    // Enqueue all other possibilities
                    for (int i = 0; i < current.Item2.Count; ++i)
                    {
                        var next = current.Item1.Select(o => o).ToList();
                        next.Add(current.Item2[i]);
                        var remaining = current.Item2.Skip(i + 1).ToList();
                        queue.Enqueue((next, remaining));
                    }
                }

                // All done
                return count.ToString();
            }
            return "";
        }
    }   
}