using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201919 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        public Day201919()
        {
            Codename = "2019-19";
            Name = "Tractor Beam";
        }

        public void Init()
        {
            _cpu = new IntCpu();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int sum = 0;
                for (int y = 0; y < 50; ++y)
                {
                    for (int x = 0; x < 50; ++x)
                    {
                        sum += (int)Probe(x, y);
                    }
                }

                return sum.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Start at x = 5 because the first 5 columns are edge case : there's no beam on them
                int x = 5;
                int min = 0;
                int max = 0;
                Queue<int> maxes = new Queue<int>();

                // Initialisation : run the loop at least 99 times
                while (maxes.Count < 99)
                {
                    (min, max) = ScanColumn(x, min, max);
                    maxes.Enqueue(max);
                    x++;
                }
                
                // Search for a place where max(x - 100) > 99 + min(x)
                while (true)
                {
                    (min, max) = ScanColumn(x, min, max);
                    int start = maxes.Dequeue();
                    if (start - min >= 99)
                    {
                        return (((x - 99) * 10000) + min).ToString();
                    }

                    maxes.Enqueue(max);
                    x++;
                }
            }

            return "";
        }

        private int LookAside(int x, int y, int state, int increment)
        {
            int newy = y;
            while (true)
            {
                int current = (int)Probe(x, newy);
                if (current == state)
                    return newy;

                newy += increment;
                if (newy < 0)
                    return 0;
            }
        }

        private long Probe(int x, int y)
        {
            _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
            _cpu.Input.Enqueue(x);
            _cpu.Input.Enqueue(y);
            _cpu.Run(true);
            return _cpu.Output.Dequeue();
        }

        private (int, int) ScanColumn(int x, int ymin, int ymax)
        {
            // Find the min value in the tractor beam, based on the previous value
            int y = ymin;
            y = LookAside(x, y, 0, -1);
            y = LookAside(x, y + 1, 1, +1);
            int newMin = y;

            // Find the max value in the tractor beam, based on the previous value
            y = 1 + Math.Max(y, ymax);
            y = LookAside(x, y, 0, +1);
            int newMax = y - 1;

            return (newMin, newMax);
        }
    }   
}