using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201817 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<bool> _clay;

        private int _top;

        private int _depth;

        private int _left;

        private int _right;

        private int _flowing;

        private int _stagning;        

        public Day201817()
        {
            Codename = "2018-17";
            Name = "Reservoir Research";
        }

        public void Init()
        {
            _clay = new Board<bool>();
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            foreach (string line in input)
            {
                // x=565, y=1098..1105  ->  "x", "565", "", "y", "1098", "", "1105"
                string[] items = line.Split('=', ',', ' ', '.');
                if (items[0] == "x")
                {
                    int x = int.Parse(items[1]);
                    int y1 = int.Parse(items[4]);
                    int y2 = int.Parse(items[6]);
                    for (int y = y1; y <= y2; ++y)
                    {
                        _clay[x, y] = true;
                    }
                }
                else
                {
                    int y = int.Parse(items[1]);
                    int x1 = int.Parse(items[4]);
                    int x2 = int.Parse(items[6]);
                    for (int x = x1; x <= x2; ++x)
                    {
                        _clay[x, y] = true;
                    }
                }
            }
            _top = _clay.Cells.Min(cell => cell.Item1.Y);
            _depth = _clay.Cells.Max(cell => cell.Item1.Y);
            _left = _clay.Cells.Min(cell => cell.Item1.X) - 1;
            _right = _clay.Cells.Max(cell => cell.Item1.X) + 1;
            Go();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return (_flowing + _stagning).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _stagning.ToString();
            }

            return "";
        }

        private void Go()
        {
            Board<bool> flowing = new Board<bool>();
            Board<bool> stagning = new Board<bool>();
            flowing[500, 0] = true;
            long waterCount = 0;
            while (true)
            {
                // Check if water is still moving
                var current = flowing.Cells.OrderByDescending(cell => cell.Item1.Y).ToList();
                if (current.Count + stagning.Values.Count == waterCount)
                {
                    // Dump(flowing, stagning, _clay);
                    break;
                }

                // Let it flow
                waterCount = current.Count + stagning.Values.Count;
                flowing = Flow(current, flowing, stagning);
            }

            _flowing = flowing.Cells.Count(c => c.Item2 && c.Item1.Y >= _top);
            _stagning = stagning.Cells.Count(c => c.Item2 && c.Item1.Y >= _top);
        }

        private Board<bool> Flow(List<(Point, bool)> current, Board<bool> flowing, Board<bool> stagning)
        {
            Board<bool> newFlowing = new Board<bool>();
            foreach (var drop in current)
            {
                if (drop.Item1.Y == _depth)
                {
                    // Out of range
                    continue;
                }

                if (!_clay[drop.Item1.X, drop.Item1.Y + 1] && !stagning[drop.Item1.X, drop.Item1.Y + 1])
                {
                    // Normal flow                    
                    newFlowing[drop.Item1.X, drop.Item1.Y + 1] = true;
                    continue;
                }

                // The deeper cell is either water or clay, we have to flow horizontally if there are borders
                if (HasBorder(drop.Item1, stagning, out int min, out int max))
                {
                    for (int x = min + 1; x < max; ++x)
                    {
                        stagning[x, drop.Item1.Y] = true;
                    }
                }
                else
                {
                    if (!_clay[drop.Item1.X - 1, drop.Item1.Y])
                    {
                        newFlowing[drop.Item1.X - 1, drop.Item1.Y] = true;
                    }
                    if (!_clay[drop.Item1.X + 1, drop.Item1.Y])
                    {
                        newFlowing[drop.Item1.X + 1, drop.Item1.Y] = true;
                    }
                }
            }

            // Make sure water is flowing eternally
            newFlowing[500, 0] = true;
            return newFlowing;
        }

        private bool HasBorder(Point from, Board<bool> stagning, out int min, out int max)
        {
            min = -1;
            max = -1;

            for (int x = from.X + 1; x <= _right; ++x)
            {
                if (_clay[x, from.Y])
                {
                    max = x;
                    break;
                }
            }

            for (int x = from.X - 1; x >= _left; --x)
            {
                if (_clay[x, from.Y])
                {
                    min = x;
                    break;
                }
            }

            if (min >= 0 && max >= 0)
            {
                for (int x = min + 1; x < max; ++x)
                {
                    if (!_clay[x, from.Y + 1] && !stagning[x, from.Y + 1])
                    {
                        return false;
                    }
                }
            }

            return (min >= 0 && max >= 0);
        }

        private void Dump(Board<bool> flowing, Board<bool> stagning, Board<bool> clay)
        {
            Console.WriteLine("");
            for (int y = 0; y <= _depth; ++y)
            {
                for (int x = _left; x <= _right; ++x)
                {
                    if (clay[x, y])
                    {
                        Console.Write("#");
                    }
                    else if (flowing[x, y])
                    {
                        Console.Write("|");
                    }                    
                    else if (stagning[x, y])
                    {
                        Console.Write("~");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                    Console.WriteLine($"{x},{y}");
                }
                Console.WriteLine("");
            }
        }
    }
}