using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201622 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Node
        {
            public int X { get; set; }

            public int Y { get; set; }

            public long Size { get; set; }

            public long Used { get; set; }

            public long Available
            {
                get
                {
                    return Size - Used;
                }
            }
        }

        private string[] _input;
        
        private List<Node> _grid;

        public Day201622()
        {
            Codename = "2016-22";
            Name = "Grid Computing";
        }


        public void Init()
        {
            _grid = new List<Node>();
            _input = Aoc.Framework.Input.GetStringVector(this);
            for (int i = 2; i < _input.Length; ++i)
            {
                // Parse a line like the following to a Node object
                // /dev/grid/node-x0-y0     85T   64T    21T   75%
                string[] items = _input[i].Split(" ").Where(item => !string.IsNullOrEmpty(item)).ToArray();

                // Get the coordinates
                string[] coordinates = items[0].Split("-");
                int x = int.Parse(coordinates[1].Substring(1));
                int y = int.Parse(coordinates[2].Substring(1));

                // Get the sizes
                int size = int.Parse(items[1][0..^1]);
                int used = int.Parse(items[2][0..^1]);

                // Build the node
                _grid.Add(new Node { X = x, Y = y, Size = size, Used = used });
            }
            _grid = _grid.OrderBy(a => a.Y).ThenBy(a => a.X).ToList();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Build the list of viable pairs
                var pair = from n1 in _grid from n2 in _grid where n1 != n2 && n1.Used <= n2.Available && n1.Used != 0 select new { n1, n2 };
               
                // Count them
                return pair.Count().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Moves count
                int count = 0;

                // Get the max X
                int MaxX = _grid.Max(n => n.X);

                // Get the free node
                Node empty = _grid.Where(n => n.Used == 0).First();

                // Moves needed to avoid the "big nodes" wall
                int big = 5;

                // Move the empty node left to the target
                count += 2 * big + empty.Y + (MaxX - empty.X - 1);

                // Print();
                return (count + 5 * (MaxX - 1) + 1).ToString();
            }

            return "";
        }

        public void Print()
        {
            var emptyNode = _grid.FirstOrDefault(a => a.Used == 0);
            var ymax = _grid.Max(n => n.Y);
            var xmax = _grid.Max(n => n.X);
            for (int x = 0; x < xmax + 1; x++)
            {
                Console.Write($" {x:00}");
            }
            Console.WriteLine();
            int i = 0;
            for (int y = 0; y < ymax + 1; y++)
            {
                for (int x = 0; x < xmax + 1; x++)
                {
                    var n = _grid[i];
                    if (n.Used == 0)
                    {
                        Console.Write(" _ ");
                    }
                    else if (n.Used > emptyNode.Size)
                    {
                        Console.Write(" # ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                    i++;
                }
                Console.Write($" {(i / (xmax + 1)) - 1}");
                Console.WriteLine();
            }
        }
    }   
}