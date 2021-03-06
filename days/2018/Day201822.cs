using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201822 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum CellType
        {
            Unknown = 0,
            Rocky = 1,
            Wet = 2,
            Narrow = 3
        }

        public enum Tools
        {
            Neither = 0,            
            ClimbingGear = 1,
            Torch = 2
        }

        public class Cell
        {
            public int GeologicalIndex { get; set; }

            public Cell(int geologicalIndex)
            {
                GeologicalIndex = geologicalIndex;
            }

            public int ErosionLevel(int depth) => (GeologicalIndex + depth) % 20183;

            public CellType Type(int depth)
            {
                return (ErosionLevel(depth) % 3) switch
                {
                    0 => CellType.Rocky,
                    1 => CellType.Wet,
                    2 => CellType.Narrow,
                    _ => CellType.Unknown,
                };
            }

            public bool Accept(int depth, Tools tool)
            {
                return (Type(depth)) switch
                {
                    CellType.Rocky => tool == Tools.ClimbingGear || tool == Tools.Torch,
                    CellType.Wet => tool == Tools.ClimbingGear || tool == Tools.Neither,
                    CellType.Narrow => tool == Tools.Neither || tool == Tools.Torch,
                    _ => false,
                };
            }
        }

        private int _depth;

        private int _width;

        private int _height;

        private Point _target;

        public Day201822()
        {
            Codename = "2018-22";
            Name = "Mode Maze";
        }

        public void Init()
        {            
            _depth = 11820;
            _width = 50;
            _height = 1000;
            _target = new Point(7,782);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Board2D<Cell> map = new Board2D<Cell>();
                long risk = 0;
                for (int y = 0; y <= _target.Y; ++y)
                {
                    for (int x = 0; x <= _target.X; ++x)
                    {
                        map[x, y] = new Cell(GetGeologicalIndex(map, _depth, x, y));
                        risk += (long)map[x,y].Type(_depth) - 1;
                    }
                }
                return risk.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board2D<Cell> map = new Board2D<Cell>();
                Board<int> distances = new Board<int>();
                PriorityQueue<(int, int, int)> queue = new PriorityQueue<(int, int, int)>();
                for (int y = 0; y <= _height; ++y)
                {
                    for (int x = 0; x <= _width; ++x)
                    {
                        map[x, y] = new Cell(GetGeologicalIndex(map, _depth, x, y));
                        for (int t = 0; t <= 2; ++t)
                        {
                            distances[new long[] {x, y, t}] = int.MaxValue;
                        }
                    }
                }

                // Initial distance : torch
                distances[new long[] { 0, 0, (int)Tools.Torch }] = 0;
                queue.Enqueue((0, 0, 2), 0);

                // Process the queue
                while (queue.TryDequeueMin(out var from))
                {
                    // Update disatnce of reachable state
                    CheckNeighboor(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3, -1,  0);
                    CheckNeighboor(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3,  1,  0);
                    CheckNeighboor(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3,  0, -1);
                    CheckNeighboor(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3,  0,  1);
                    CheckToolSwitch(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3, 0);
                    CheckToolSwitch(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3, 1);
                    CheckToolSwitch(map, distances, queue, _depth, from.Item1, from.Item2, from.Item3, 2);
                }

                return distances[new long[] { _target.X, _target.Y, (int)Tools.Torch }].ToString();
            }

            return "";
        }

        private void CheckNeighboor(Board2D<Cell> map, Board<int> distances, PriorityQueue<(int, int, int)> queue, int depth, int x, int y, int z, int xoffset, int yoffset)
        {
            // Off the grid
            if ((x + xoffset < 0) || (y + yoffset < 0) || (x + xoffset > _width) || (y + yoffset > _height))
            {
                return;
            }

            // Check that we have the right tool for the neighboors
            if (map[x + xoffset, y + yoffset].Accept(depth, (Tools)z))
            {
                int d = distances[new long[] { x, y, z }];
                if (distances[new long[] { x + xoffset, y + yoffset, z }] > 1 + d)
                {
                    // Shortest path found
                    distances[new long[] { x + xoffset, y + yoffset, z }] = 1 + d;

                    // Update priority queue
                    queue.EnqueueOrUpdate((x + xoffset, y + yoffset, z), 1 + d);
                }
            }
        }

        private void CheckToolSwitch(Board2D<Cell> map, Board<int> distances, PriorityQueue<(int, int, int)> queue, int depth, int x, int y, int z, int tool)        
        {
            // This is the same tool
            if (z == tool)
            {
                return;    
            }

            // Check that we have the right tool for the neighboors
            if (map[x, y].Accept(depth, (Tools)tool))
            {
                int d = distances[new long[] { x, y, z }];
                if (distances[new long[] { x, y, tool }] > 7 + d)
                {
                    // Shortest path found
                    distances[new long[] { x, y, tool }] = 7 + d;

                    // Update priority queue
                    queue.EnqueueOrUpdate((x, y, tool), 7 + d);
                }
            }
        }

        private int GetGeologicalIndex(Board2D<Cell> map, int depth, int x, int y)
        {
            if (x == 0 && y == 0)
            {
                return 0;
            }

            if (x == _target.X && y == _target.Y)
            {
                return 0;
            }

            if (y == 0)
            {
                return x * 16807;
            }

            if (x == 0)
            {
                return y * 48271;
            }

            return map[x - 1, y].ErosionLevel(depth) * map[x, y - 1].ErosionLevel(depth);
        }
    }   
}