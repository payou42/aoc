using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201619 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _count;

        public Day201619()
        {
            Codename = "2016-19";
            Name = "An Elephant Named Joseph";
        }

        public void Init()
        {
            _count = Aoc.Framework.Input.GetInt(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Prepare the data
                Queue<(long, long)> queue = new Queue<(long, long)>();
                for (int i = 0; i < _count; ++i)
                {
                    queue.Enqueue((i + 1, 1));
                }

                // Process the queue
                while (queue.TryDequeue(out var elf))
                {
                    if (!queue.TryDequeue(out var next))
                    {
                        // The elf is the last one, he won
                        return elf.Item1.ToString();
                    }

                    // Steal the presents !
                    queue.Enqueue((elf.Item1, elf.Item2 + next.Item2));
                }

                return "not found";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // The same with another structure
                SparseArray<long> elves = new SparseArray<long>(_count);
                for (long i = 0; i < elves.Length; ++i)
                {
                    elves[i] = 1;
                }

                // Process the array
                long current = 0;
                long opposite = elves.RealLength / 2;
                while (elves.RealLength > 1)
                {
                    elves[current] += elves[opposite];
                    elves.RemoveAt(opposite);
                    if (elves.RealLength % 2 == 0)
                    {
                        opposite = elves.Next(opposite, 2);
                    }
                    else
                    {
                        opposite = elves.Next(opposite, 1);
                    }
                    current = elves.Next(current);
                }

                return (current + 1).ToString();
            }

            return "";
        }
    }   
}