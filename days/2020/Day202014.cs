using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202014 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day202014()
        {
            Codename = "2020-14";
            Name = "Docking Data";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            string mask = "";
            Dictionary<long, long> mem = new Dictionary<long, long>();
            foreach (string s in _input)
            {
                // Process mask
                if (s.StartsWith("mask = "))
                {
                    mask = s[7..];
                    continue;
                }

                // Process mem storage
                string[] items = s.Split('[', ']');
                long addr = long.Parse(items[1]);
                long val = long.Parse(items[2][3..]);

                if (part == Aoc.Framework.Part.Part1)
                {
                    this.WriteMemory1(mem, addr, val, mask);
                }
                else
                {
                    this.WriteMemory2(mem, addr, val, mask);
                }

            }

            return mem.Values.Sum().ToString();
        }

        private void WriteMemory1(Dictionary<long, long> mem, long addr, long val, string mask)
        {
            // Apply mask
            for (int i = 0; i < 36; ++i)
            {
                switch (mask[^(i + 1)])
                {
                    case '0': val &= (~(1L << i) & ((1L << 36) - 1)); break;
                    case '1': val |= (1L << i); break;
                }
            }

            // Store in memory
            mem[addr] = val;
        }

        private void WriteMemory2(Dictionary<long, long> mem, long addr, long val, string mask)
        {
            // Apply mask
            List<int> floating = new List<int>();
            for (int i = 0; i < 36; ++i)
            {
                switch (mask[^(i + 1)])
                {
                    case 'X':
                    {
                        floating.Add(i);
                        break;
                    }
                    
                    case '1':
                    {
                        addr |= (1L << i);
                        break;
                    }
                }
            }

            // Store in memory
            Queue<(long, List<int>)> pendingWrites = new Queue<(long, List<int>)>();
            pendingWrites.Enqueue((addr, floating));
            while (pendingWrites.Count > 0)
            {
                var (a, l) = pendingWrites.Dequeue();
                if (l.Count == 0)
                {
                    mem[a] = val;
                }
                else
                {
                    // Get the index to process
                    int index = l.First();

                    // Enqueue the 2 possible addresses
                    long a1 = a |= 1L << index;
                    var remainer1 = l.Skip(1).ToList();
                    pendingWrites.Enqueue((a1, remainer1));

                    long a2 = a & (~(1L << index) & ((1L << 36) - 1));
                    var remainer2 = l.Skip(1).ToList();
                    pendingWrites.Enqueue((a2, remainer2));
                }
            }
        }
    }   
}