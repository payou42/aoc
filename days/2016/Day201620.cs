using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201620 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _blocked;

        public class Block
        {
            public long From { get; set; }

            public long To { get; set; }
        }

        private List<Block> _ips;

        public Day201620()
        {
            Codename = "2016-20";
            Name = "Firewall Rules";
        }

        public void Init()
        {
            _blocked = Aoc.Framework.Input.GetStringVector(this);
            _ips = new List<Block> { new Block { From = 0, To = 4294967295 } };
            foreach (string range in _blocked)
            {
                string[] items = range.Split("-");
                long from = long.Parse(items[0]);
                long to = long.Parse(items[1]);

                foreach (Block b in _ips.ToList())
                {
                    if (from > b.To || to < b.From)
                    {
                        continue;
                    }

                    if (from <= b.From && to >= b.To)
                    {
                        _ips.Remove(b);
                        continue;
                    }

                    if (from > b.From && to < b.To)
                    {
                        // Divide the block
                        _ips.Add(new Block { From = to + 1, To = b.To });
                        b.To = from - 1;
                        continue;
                    }

                    if (from > b.From)
                    {
                        b.To = from - 1;
                        continue;
                    }

                    if (to < b.To)
                    {
                        b.From = to + 1;
                        continue;
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _ips.Select(block => block.From).Min().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _ips.Select(block => 1 + block.To - block.From).Sum().ToString();
            }

            return "";
        }
    }   
}