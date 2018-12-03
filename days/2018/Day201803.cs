using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201803 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Rectangle[] _slices;

        private bool[] _intersects;

        private Board<bool> _board;

        public Day201803()
        {
            Codename = "2018-03";
            Name = "No Matter How You Slice It";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this, "\n");
            _slices = _input.Select(s => ParseRectangle(s)).ToArray();
            _board = new Board<bool>();
            _intersects = new bool[_slices.Length];
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Intersection counter
                long count = 0;

                // Intersect all the Rectangle and mark the zones in the board
                for (int i = 0; i < _slices.Length - 1; ++i)
                {
                    for (int j = i + 1; j < _slices.Length; ++j)
                    {
                        Rectangle inter = Rectangle.Intersect(_slices[i], _slices[j]);

                        for (int x = inter.Left; x < inter.Left + inter.Width; ++x)
                        {
                            for (int y = inter.Top; y < inter.Top + inter.Height; ++y)
                            {
                                if (!_board[x, y])
                                {
                                    count++;
                                    _board[x, y] = true;
                                }
                            }
                        }

                        if (inter.Width > 0)
                        {
                            _intersects[i] = true;
                            _intersects[j] = true;
                        }
                    }
                }

                // All done !
                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                for (int i = 0; i < _intersects.Length; ++i)
                {
                    if (!_intersects[i])
                    {
                        return (i + 1).ToString();
                    }
                }
                return "not found";
            }

            return "";
        }

        private Rectangle ParseRectangle(string input)
        {
            string[] items = input.Split(" ");
            string[] pos = items[2].Split(",");
            int x = int.Parse(pos[0]);
            int y = int.Parse(pos[1].Substring(0, pos[1].Length - 1));

            string[] size = items[3].Split("x");
            int w = int.Parse(size[0]);
            int h = int.Parse(size[1]);

            return new Rectangle(x, y, w, h);
        }
    }   
}