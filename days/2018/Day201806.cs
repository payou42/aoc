using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day201806 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[][] _destinations;

        private int _maxX;

        private int _maxY;

        public Day201806()
        {
            Codename = "2018-06";
            Name = "Chronal Coordinates";
        }

        public void Init()
        {
            _destinations = Aoc.Framework.Input.GetIntMatrix(this, ", ");
            _maxX = _destinations.Max(p => p[0]);
            _maxY = _destinations.Max(p => p[1]);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return GetLargestRegionSize().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return GetSafestRegionSize().ToString();
            }

            return "";
        }

        private long GetLargestRegionSize()
        {
            Board2D<int> board = new Board2D<int>();

            // Build the map
            for (int x = 0; x <= _maxX; ++x)
            {
                for (int y = 0; y <= _maxY; ++y)
                {
                    board[x, y] = GetClosest(x, y);
                }
            }

            // Exclude region on the edges
            HashSet<int> excluded = GetInfiniteRegions(board);            

            // Calculate the size of the non-excluded regions
            return _destinations.Select((n, i) => GetRegionSize(board, i, excluded)).Max();            
        }

        private long GetSafestRegionSize()
        {
            // Counter
            long safeCount = 0;

            // Count the safe spots
            for (int x = 0; x <= _maxX; ++x)
            {
                for (int y = 0; y <= _maxY; ++y)
                {
                    if (IsSafe(x, y, 10000))
                    {
                        safeCount++;
                    }
                }
            }

            // All done
            return safeCount;
        }        

        private int GetClosest(int x, int y)
        {
            int[] dest = _destinations.Select(p => Math.Abs(p[0] - x) + Math.Abs(p[1] - y)).ToArray();
            int min = dest.Min();
            int[] closest = dest.Select((dist, index) => (dist, index)).Where(d => d.dist == min).Select(d => d.index).ToArray();
            if (closest.Length == 1)
            {
                return closest[0];
            }
            return -1;            
        }

        private HashSet<int> GetInfiniteRegions(Board2D<int> board)
        {
            HashSet<int> infinite = new HashSet<int>();
            for (int x = 0; x <= _maxX; ++x)
            {
                infinite.Add(board[x, 0]);
                infinite.Add(board[x, _maxY]);
            }
            for (int y = 0; y <= _maxY; ++y)
            {
                infinite.Add(board[0, y]);
                infinite.Add(board[_maxX, y]);
            }
            return infinite;
        }

        private long GetRegionSize(Board2D<int> board, int index, HashSet<int> excluded = null)
        {
            if (excluded != null && excluded.Contains(index))
            {
                return (-1);
            }

            // Count the region
            return board.Values.Count(cell => cell == index);
        }

        private bool IsSafe(int x, int y, int max)
        {
            return _destinations.Sum(p => Math.Abs(p[0] - x) + Math.Abs(p[1] - y)) < max; 
        }
    }   
}