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
                        return Scrambling.ReplacePosition(password, int.Parse(items[2]), int.Parse(items[5]));
                    }
                    if (items[1] == "letter")
                    {
                        return Scrambling.ReplaceLetter(password, items[2][0], items[5][0]);
                    }
                    break;
                }

                case "rotate":
                {
                    if (items[1] == "left")
                    {
                        return Scrambling.RotateLeft(password, int.Parse(items[2]));
                    }
                    else if (items[1] == "right")
                    {
                        return Scrambling.RotateRight(password, int.Parse(items[2]));
                    }
                    else if (items[1] == "based")
                    {
                        return Scrambling.RotateLetter(password, items[6][0]);
                    }
                    break;
                }

                case "reverse":
                {
                    return Scrambling.ReversePart(password, int.Parse(items[2]), int.Parse(items[4]));
                }

                case "move":
                {
                    return Scrambling.MovePart(password, int.Parse(items[2]), int.Parse(items[5]));
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
                        return Scrambling.ReplacePosition(password, int.Parse(items[2]), int.Parse(items[5]));
                    }
                    if (items[1] == "letter")
                    {
                        return Scrambling.ReplaceLetter(password, items[2][0], items[5][0]);
                    }
                    break;
                }

                case "rotate":
                {
                    if (items[1] == "right")
                    {
                        return Scrambling.RotateLeft(password, int.Parse(items[2]));
                    }
                    else if (items[1] == "left")
                    {
                        return Scrambling.RotateRight(password, int.Parse(items[2]));
                    }
                    else if (items[1] == "based")
                    {
                        char c = items[6][0];
                        for (int i = 0; i < password.Length; ++i)
                        {
                            var result = Scrambling.RotateRight(password, i);
                            if (password == Scrambling.RotateLetter(result, c))
                            {
                                return result;
                            }
                        }                        
                    }
                    break;
                }

                case "reverse":
                {
                    return Scrambling.ReversePart(password, int.Parse(items[2]), int.Parse(items[4]));
                }

                case "move":
                {
                    return Scrambling.MovePart(password, int.Parse(items[5]), int.Parse(items[2]));
                }
            }
            return password;
        }        
    }   
}