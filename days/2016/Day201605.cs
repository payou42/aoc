using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Crypto;

namespace Aoc
{
    public class Day201605 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201605()
        {
            Codename = "2016-05";
            Name = "How About a Nice Game of Chess?";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Int64 index = 0;
                string password = "";
                string key = Aoc.Framework.Input.GetString(this);
                while (password.Length < 8)
                {
                    string hash = Md5.Compute(key, index);
                    if (hash.StartsWith("00000"))
                    {
                        password += hash[5];
                    }
                    index++;
                }

                return password;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Int64 index = 0;
                int count = 0;
                bool[] found = new bool[8] {false, false, false, false, false, false, false, false};
                char[] password = new char[8] {'0', '0', '0', '0', '0', '0', '0', '0'};
                string key = Aoc.Framework.Input.GetString(this);
                while (count < 8)
                {
                    string hash = Md5.Compute(key, index);
                    if (hash.StartsWith("00000"))
                    {
                        if (Int32.TryParse(hash.Substring(5, 1), out int position))
                        {
                            if (position < 8 && !found[position])
                            {
                                found[position] = true;
                                password[position] = hash[6];
                                count++;
                            }
                        }
                    }
                    index++;
                }

                return new String(password);
            }

            return "";
        }
    }
}