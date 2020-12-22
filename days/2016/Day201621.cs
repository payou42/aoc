using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Strings;

namespace Aoc
{
    public class Day201621 : Aoc.Framework.IDay
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
                string password = "abcdefgh";
                foreach (string s in _instructions)
                {
                    password = Scramble(s, password);
                }
                return password;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                string password = "fbgdceah";
                for (int i = _instructions.Length - 1; i >= 0; --i)
                {
                    password = Unscramble(_instructions[i], password);
                }
                return password;
            }

            return "";
        }

        private string Scramble(string instruction, string password)
        {
            string[] items = instruction.Split(" ");
            switch (items[0])
            {
                case "swap":
                {
                    if (items[1] == "position")
                    {
                        return password.ReplacePosition(int.Parse(items[2]), int.Parse(items[5]));
                    }
                    if (items[1] == "letter")
                    {
                        return password.ReplaceLetter(items[2][0], items[5][0]);
                    }
                    break;
                }

                case "rotate":
                {
                    if (items[1] == "left")
                    {
                        return password.RotateLeft(int.Parse(items[2]));
                    }
                    else if (items[1] == "right")
                    {
                        return password.RotateRight(int.Parse(items[2]));
                    }
                    else if (items[1] == "based")
                    {
                        return password.RotateLetter(items[6][0]);
                    }
                    break;
                }

                case "reverse":
                {
                    return password.ReversePart(int.Parse(items[2]), int.Parse(items[4]));
                }

                case "move":
                {
                    return password.MovePart(int.Parse(items[2]), int.Parse(items[5]));
                }
            }
            return password;
        }

        private string Unscramble(string instruction, string password)
        {
            string[] items = instruction.Split(" ");
            switch (items[0])
            {
                case "swap":
                {
                    if (items[1] == "position")
                    {
                        return password.ReplacePosition(int.Parse(items[2]), int.Parse(items[5]));
                    }
                    if (items[1] == "letter")
                    {
                        return password.ReplaceLetter(items[2][0], items[5][0]);
                    }
                    break;
                }

                case "rotate":
                {
                    if (items[1] == "right")
                    {
                        return password.RotateLeft(int.Parse(items[2]));
                    }
                    else if (items[1] == "left")
                    {
                        return password.RotateRight(int.Parse(items[2]));
                    }
                    else if (items[1] == "based")
                    {
                        char c = items[6][0];
                        for (int i = 0; i < password.Length; ++i)
                        {
                            var result = password.RotateRight(i);
                            if (password == result.RotateLetter(c))
                            {
                                return result;
                            }
                        }                        
                    }
                    break;
                }

                case "reverse":
                {
                    return password.ReversePart(int.Parse(items[2]), int.Parse(items[4]));
                }

                case "move":
                {
                    return password.MovePart(int.Parse(items[5]), int.Parse(items[2]));
                }
            }
            return password;
        }        
    }   
}