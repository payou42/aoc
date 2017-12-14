using System;
using System.Text;
using System.Collections.Generic;

namespace _14._Defragmentation
{
    class Program
    {
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

        static ConsoleColor[] colors = new ConsoleColor[10]
        {
            ConsoleColor.Blue,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkYellow,
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.White,
            ConsoleColor.DarkBlue,
            ConsoleColor.Magenta
        };

        static void DrawMap(int[,] grid)
        {
            for (int i = 0; i < 128; ++i)
            {
                for (int j = 0; j < 128; ++j)
                {                   
                    if (grid[i, j] >= 0)
                    {
                        Console.ForegroundColor = colors[grid[i, j] % colors.Length];
                        Console.Write("#");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(".");
                    }
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            // Create the hasher
            KnotHash hasher = new KnotHash();

            // Prepare the input
            string key = Input.RAW;            

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

            // Count bits
            int bits = 0;
            for (int i = 0; i < 128; ++i)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i,j] >= 0)
                    {
                        bits++;
                    }
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

            // Draw the map
            ConsoleColor original = Console.ForegroundColor;
            DrawMap(grid);
            Console.ForegroundColor = original;

            // Results
            Console.WriteLine("Bits count  : {0}", bits);            
            Console.WriteLine("Groups count: {0}", groupId - 1);
        }
    }
}
