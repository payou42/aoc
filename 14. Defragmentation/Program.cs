using System;
using System.Text;
using System.Collections.Generic;

namespace _14._Defragmentation
{
    class Program
    {
        static int CountBits(byte[] input)
        {
            int bits = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                for (int j=0; j < 8; ++j)
                {
                    bool test = ((input[i] >> (7-j)) & 1) == 1;                    
                    if (test)
                    {
                        Console.Write("#");
                        bits++;
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
            }
            Console.WriteLine("");
            return bits;
        }

        static bool IsUsed(byte[] hash, int index)
        {
            byte b = hash[index / 8];
            return ((b >> (7 - (index % 8))) & 1) == 1;
        }

        static void BuildGroup(int[,] grid, int x, int y, int id)
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

        static void Main(string[] args)
        {
            // Create the hasher
            KnotHash hasher = new KnotHash();

            // Process the input
            string key = Input.RAW;
            int bits = 0;
            for (int i = 0; i < 128; ++i)
            {
                string row = key + "-" + i.ToString();
                bits += CountBits(hasher.Compute(row));
            }            
            Console.WriteLine("Count bits: {0}", bits);

            // Build the map of used cells
            int[,] grid = new int[128, 128];
            for (int i = 0; i < 128; ++i)
            {
                string row = key + "-" + i.ToString();
                byte[] hash = hasher.Compute(row);
                for (int j = 0; j < 128; j++)
                {
                    grid[i, j] = IsUsed(hash, j) ? 0 : -1;
                }
            }

            // Build groups
            int groupId = 1;
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        BuildGroup(grid, i, j, groupId++);
                    }
                }
            }
            Console.WriteLine("Groups count: {0}", groupId - 1);
        }
    }
}
