using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201518 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<bool> _grid;

        private string[] _input;

        public Day201518()
        {
            Codename = "2015-18";
            Name = "Like a GIF For Your Yard";
        }

        public void Init()
        {
            _grid = new Board<bool>();
            _input = Aoc.Framework.Input.GetStringVector(this);
            for (int y = 0; y < _input.Length; ++y )
            {
                for (int x = 0; x < _input[y].Length; ++x)
                {
                    if (_input[y][x] == '#')
                    {
                        _grid[x, y] = true;
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {                
                Board<bool> current = _grid;
                for (int i = 0; i < 100; ++i)
                {
                    // Evaluate next step
                    Board<bool> next = new Board<bool>();
                    for (int x = 0; x < 100; ++x)
                    {
                        for (int y = 0; y < 100; ++y)
                        {
                            if (GetNextState(current, x, y))
                            {
                                next[x, y] = true;
                            }
                        }
                    }
                    current = next;
                }
                return current.Values.Count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board<bool> current = _grid;
                for (int i = 0; i < 100; ++i)
                {
                    // Force the corners to be on
                    current[0, 0] = true;
                    current[0, 99] = true;
                    current[99, 0] = true;
                    current[99, 99] = true;

                    // Evaluate next step
                    Board<bool> next = new Board<bool>();
                    for (int x = 0; x < 100; ++x)
                    {
                        for (int y = 0; y < 100; ++y)
                        {
                            if (GetNextState(current, x, y))
                            {
                                next[x, y] = true;
                            }
                        }
                    }
                    current = next;
                }

                // Force the corners to be on
                current[0, 0] = true;
                current[0, 99] = true;
                current[99, 0] = true;
                current[99, 99] = true;
                return current.Values.Count.ToString();
            }

            return "";
        }

        private bool GetNextState(Board<bool> board, int x, int y)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; ++i)
            {
                for (int j = y - 1; j <= y + 1; ++j)
                {
                    if (board[i, j])
                    {
                        count++;
                    }
                }
            
            }
            if (board[x, y])
            {
                return (count == 3) || (count == 4);
            }
            else
            {
                return count == 3;
            }
        }
    }   
}