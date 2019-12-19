using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201811 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _input;

        private Board<int> _cells;

        public Day201811()
        {
            Codename = "2018-11";
            Name = "Chronal Charge";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetInt(this);
            _cells = new Board<int>();
            BuildPowerLevels();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
               // Find the best power cell
               var best = FindBestCell(3);
               return $"{best.Item1},{best.Item2}";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                (Point, int) best = (new Point(0, 0), 0);
                Board<int> power = new Board<int>();                
                int bestSize = 0;
                // Find the best power cell
                for (int size = 1; size <= 30; ++size)
                {
                    IncrementPowerGrid(power, size);                    
                    var result = power.Cells.OrderByDescending(cell => cell.Item2).First();
                    if (result.Item2 > best.Item2)
                    {
                        best = result;
                        bestSize = size;
                    }
                }
                return $"{best.Item1.X},{best.Item1.Y},{bestSize}";               
            }
            return "";
        }

        private void BuildPowerLevels()
        {
            for (int x = 1; x <= 300; ++x)
            {
                for (int y = 1; y <= 300; ++y)
                {
                    int rack = 10 + x;
                    int power = rack * ((rack * y) + _input);
                    power = ((power / 100) % 10) - 5;
                    _cells[x, y] = power;
                }
            }
        }

        private (int, int, int) FindBestCell(int size)
        {
            int bestValue = 0;
            int bestX = 0;
            int bestY = 0;
            for (int x = 1; x <= 300; ++x)
            {
                for (int y = 1; y <= 300; ++y)
                {
                    int v = GetPower(x, y, size);
                    if (v > bestValue)
                    {
                        bestValue = v;
                        bestX = x;
                        bestY = y;
                    }
                }
            }
            return (bestX, bestY, bestValue);
        }

        private void IncrementPowerGrid(Board<int> power, int size)
        {
            for (int x = 1; x <= 300; ++x)
            {
                for (int y = 1; y <= 300; ++y)
                {                    
                    int increment = 0;
                    
                    int xx = x + size - 1;
                    if (xx <= 300)
                    {
                        for (int j = y; j < y + size; ++j)
                        {
                            increment += _cells[xx, j];
                        }
                    }

                    int yy = y + size - 1;
                    if (yy <= 300)
                    {
                        for (int i = x; i < x + size - 1; ++i)
                        {
                            increment += _cells[i, yy];
                        }
                    }
                    power[x, y] += increment;
                }
            }            
        }

        private int GetPower(int x, int y, int size)
        {
            int power = 0;
            int mx = Math.Min(300, x + size);
            int my = Math.Min(300, y + size);
            for (int i = x; i < mx; ++i)
            {
                for (int j = y; j < my; ++j)
                {
                    power += _cells[i, j]; 
                }
            }
            return power;
        }
    }   
}