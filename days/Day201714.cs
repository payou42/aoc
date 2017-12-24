using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201714 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _bits;

        private int _groupId;        

        public Day201714()
        {
            Codename = "2017-14";
            Name = "Disk Defragmentation";
        }

        public void Init()
        { 
            BuildMap();
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _bits.ToString();
            }

            if (part == Part.Part2)
            {
                return (_groupId - 1).ToString();
            }

            return "";
        }

        private void BuildMap()
        {
            // Create the hasher
            KnotHash hasher = new KnotHash();

            // Prepare the input
            string key = Input.GetString(this);            

            // Build the map of used cells
            int[,] grid = new int[128, 128];
            for (int i = 0; i < 128; ++i)
            {
                string row = key + "-" + i.ToString();
                hasher.Compute(row, true);
                byte[] hash = hasher.GetBytesHash();
                for (int j = 0; j < 128; j++)
                {
                    grid[i, j] = IsUsed(hash, j) ? 0 : -1;
                }
            }

            // Count bits
            _bits = 0;
            for (int i = 0; i < 128; ++i)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i,j] >= 0)
                    {
                        _bits++;
                    }
                }
            }  

            // Build groups
            _groupId = 1;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        BuildGroup(grid, i, j, _groupId++);
                    }
                }
            }            
        }

        private bool IsUsed(byte[] hash, int index)
        {
            byte b = hash[index / 8];
            return ((b >> (7 - (index % 8))) & 1) == 1;
        }

        private void BuildGroup(int[,] grid, int x, int y, int id)
        {
            // Set the coordinates to belong to the current group
            grid[x, y] = id;

            // Propagate to nearby cell
            if ((x > 0) && (grid[x - 1, y] == 0))
            {
                BuildGroup(grid, x - 1, y, id);
            }
            if ((y > 0) && (grid[x, y - 1] == 0))
            {
                BuildGroup(grid, x, y - 1, id);
            }
            if ((x < 127) && (grid[x + 1, y] == 0))
            {
                BuildGroup(grid, x + 1, y, id);
            }
            if ((y < 127) && (grid[x, y + 1] == 0))
            {
                BuildGroup(grid, x, y + 1, id);
            }
        }
    }   
}