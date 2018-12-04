using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201621 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _instructions;

        public Day201621()
        {
            Codename = "2016-21";
            Name = "Scrambled Letters and Hash";
        }

        public void Init()
        {
            _instructions = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return "part1";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }

        private void ProcessSingle(string instruction, ref int index, char[] password)
        {
            string[] items = instruction.Split(" ");
            switch (items[0])
            {
                case "swap":
                {
                    if (items[1] == "position")
                    {
                        int x = int.Parse(items[2]);
                        int y = int.Parse(items[5]);
                        char tmp = password[(x + index) % password.Length];
                        password[(x + index) % password.Length] = password[(y + index) % password.Length];
                        password[(y + index) % password.Length] = tmp;
                    }
                    else if (items[1] == "letter")
                    {
                        char x = items[2][0];
                        char y = items[5][0];
                        for (int i = 0; i < password.Length; ++i)
                        {
                            if (password[i] == x)
                            {
                                password[i] = y;
                            }
                            else if (password[i] == y)
                            {
                                password[i] = x;
                            }
                        }
                    }
                    break;
                }

                case "rotate":
                {
                    if (items[1] == "left")
                    {
                        index = (index + password.Length - int.Parse(items[2])) % password.Length;
                    }
                    else if (items[1] == "right")
                    {
                        index = (index + int.Parse(items[2])) % password.Length;
                    }
                    else if (items[1] == "based")
                    {
                        char c = items[6][0];
                        for (int i = 0; i < password.Length; ++i)
                        {
                            if (password[i] == c)
                            {
                                int offset = (i - index + password.Length) % password.Length;
                                index = i + 1 + (offset >= 4 ? 1 : 0);
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }
    }   
}