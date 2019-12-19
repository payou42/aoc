using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201503 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }
        
        public string _input;

        public Day201503()
        {
            Codename = "2015-03";
            Name = "Perfectly Spherical Houses in a Vacuum";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Initial position
                Board<int> grid = new Board<int>();
                int x = 0;
                int y = 0;
                grid[x, y] += 1;

                // Follow the instructions
                foreach (char c in _input)
                {
                    switch (c)
                    {
                        case '^':
                        {
                            y++;
                            break;
                        }

                        case 'v':
                        {
                            y--;
                            break;
                        }

                        case '>':
                        {
                            x++;
                            break;
                        }

                        case '<':
                        {
                            x--;
                            break;
                        }
                    }

                    grid[x, y] += 1;
                }

                // Count houses with gift
                return grid.Values.Where(v => v >= 1).Count().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Initial position
                Board<int> grid = new Board<int>();
                int x1 = 0;
                int y1 = 0;
                int x2 = 0;
                int y2 = 0;
                grid[0, 0] += 2;

                // Follow the instructions
                for (int i = 0; i < _input.Length; i += 2)
                {
                    switch (_input[i])
                    {
                        case '^':
                        {
                            y1++;
                            break;
                        }

                        case 'v':
                        {
                            y1--;
                            break;
                        }

                        case '>':
                        {
                            x1++;
                            break;
                        }

                        case '<':
                        {
                            x1--;
                            break;
                        }
                    }

                    switch (_input[i + 1])
                    {
                        case '^':
                        {
                            y2++;
                            break;
                        }

                        case 'v':
                        {
                            y2--;
                            break;
                        }

                        case '>':
                        {
                            x2++;
                            break;
                        }

                        case '<':
                        {
                            x2--;
                            break;
                        }
                    }                    

                    grid[x1, y1] += 1;
                    grid[x2, y2] += 1;
                }

                // Count houses with gift
                return grid.Values.Where(v => v >= 1).Count().ToString();

            }

            return "";
        }
    }   
}