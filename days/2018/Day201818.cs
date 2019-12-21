using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201818 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum Cell
        {
            Invalid = 0,
            Open = 1,
            Tree = 2,
            Lumberyard = 3,
        };

        private Board<Cell> _ground;

        public Day201818()
        {
            Codename = "2018-18";
            Name = "Settlers of The North Pole";
        }

        public void Init()
        {
            _ground = new Board<Cell>();
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    _ground[x, y] = (input[y][x] == '.' ? Cell.Open : (input[y][x] == '|' ? Cell.Tree : Cell.Lumberyard));
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Pass time
                Board<Cell> current = _ground;
                for (int i = 0; i < 10; ++i)
                {
                    current = Tick(current);
                }

                // Calculate result
                var result = current.Values.Count(v => v == Cell.Tree) * current.Values.Count(v => v == Cell.Lumberyard);
                return result.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Run a long time in order to stabilize
                Board<Cell> current = _ground;
                for (int i = 0; i < 1000; ++i)
                {
                    current = Tick(current);
                }

                // Look for a cycle
                Dictionary<string, int> recorded = new Dictionary<string, int>();
                int index = 1000;
                while (index < 1000000000)
                {
                    string hash = string.Join("", current.Cells.OrderBy(cell => cell.Item1.Y).ThenBy(cell => cell.Item1.Y).Select(cell => ((int)cell.Item2).ToString()));
                    if (recorded.ContainsKey(hash))
                    {
                        // Cycle found !
                        int start = recorded[hash];
                        int cycleLength = index - start;

                        // Advance the index
                        index += cycleLength * ((1000000000 - index) / cycleLength);

                        // Exit this loop
                        break;
                    }
                    recorded.Add(hash, index);
                    current = Tick(current);
                    index++;
                }

                // Iterate a few more times
                while (index < 1000000000)
                {
                    current = Tick(current);
                }

                // Calculate result
                var result = current.Values.Count(v => v == Cell.Tree) * current.Values.Count(v => v == Cell.Lumberyard);
                return result.ToString();
            }

            return "";
        }

        private Board<Cell> Tick(Board<Cell> current)
        {
            Board<Cell> after = new Board<Cell>();
            for (int x = 0; x < 50; ++x)
            {
                for (int y = 0; y < 50; ++y)
                {
                    switch (current[x, y])
                    {
                        case Cell.Open:
                        {
                            int trees = CountAround(current, x, y, Cell.Tree);
                            after[x, y] = trees >= 3 ? Cell.Tree : Cell.Open;
                            break;
                        }

                        case Cell.Tree:
                        {
                            int lumberyards = CountAround(current, x, y, Cell.Lumberyard);
                            after[x, y] = lumberyards >= 3 ? Cell.Lumberyard : Cell.Tree;
                            break;
                        }

                        case Cell.Lumberyard:
                        {
                            int lumberyards = CountAround(current, x, y, Cell.Lumberyard);
                            int trees = CountAround(current, x, y, Cell.Tree);
                            after[x, y] = (lumberyards >= 1 && trees >= 1) ? Cell.Lumberyard : Cell.Open;
                            break;
                        }
                    }   
                }
            }
            return after;
        }

        private int CountAround(Board<Cell> current, int x, int y, Cell cell)
        {
            // Count the cell around us
            int count = 0;
            for (int i = x - 1; i <= x + 1; ++i)
            {
                for (int j = y - 1; j <= y + 1; ++j)
                {
                    if (current[i, j] == cell)
                    {
                        count++;
                    }
                }
            }

            if (current[x, y] == cell)
            {
                count--;
            }
            
            return count;
        }
    }
}