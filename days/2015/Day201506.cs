using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201506 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        public Day201506()
        {
            Codename = "2015-06";
            Name = "Probably a Fire Hazard";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Board<bool> board = new Board<bool>();
                foreach (string instruction in _instructions)
                {
                    // Parse the instruction
                    string[] items = instruction.Split(" ");

                    // Process the instruction
                    switch (items[0])
                    {
                        // turn on 931,331 through 939,812
                        // turn off 756,53 through 923,339
                        case "turn":
                        {
                            Point from = ParsePoint(items[2]);
                            Point to = ParsePoint(items[4]);
                            bool mode = items[1] == "on";
                            for (int x = from.X; x <= to.X; ++x)
                            {
                                for (int y = from.Y; y <= to.Y; ++y)
                                {
                                    board[x, y] = mode;
                                }
                            }
                            break;
                        }

                        // toggle 756,965 through 812,992
                        case "toggle":
                        {
                            Point from = ParsePoint(items[1]);
                            Point to = ParsePoint(items[3]);
                            bool mode = items[1] == "on";
                            for (int x = from.X; x <= to.X; ++x)
                            {
                                for (int y = from.Y; y <= to.Y; ++y)
                                {
                                    board[x, y] = !board[x, y];
                                }
                            }
                            break;
                        }
                    }
                }

                return board.Values.Count(light => light).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board<int> board = new Board<int>();
                foreach (string instruction in _instructions)
                {
                    // Parse the instruction
                    string[] items = instruction.Split(" ");

                    // Process the instruction
                    switch (items[0])
                    {
                        // turn on 931,331 through 939,812
                        // turn off 756,53 through 923,339
                        case "turn":
                        {
                            Point from = ParsePoint(items[2]);
                            Point to = ParsePoint(items[4]);
                            bool mode = items[1] == "on";
                            for (int x = from.X; x <= to.X; ++x)
                            {
                                for (int y = from.Y; y <= to.Y; ++y)
                                {
                                    board[x, y] = mode ? board[x, y] + 1 : Math.Max(board[x, y] - 1, 0);
                                }
                            }
                            break;
                        }

                        // toggle 756,965 through 812,992
                        case "toggle":
                        {
                            Point from = ParsePoint(items[1]);
                            Point to = ParsePoint(items[3]);
                            bool mode = items[1] == "on";
                            for (int x = from.X; x <= to.X; ++x)
                            {
                                for (int y = from.Y; y <= to.Y; ++y)
                                {
                                    board[x, y] += 2;
                                }
                            }
                            break;
                        }
                    }
                }

                return board.Values.Sum().ToString();
            }

            return "";
        }

        private Point ParsePoint(string s)
        {
            string[] items = s.Split(",");
            return new Point(int.Parse(items[0]), int.Parse(items[1]));
        }
    }   
}