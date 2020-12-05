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
                    byte[] hash = Md5.Compute(key, index);
                    if (hash[0] == 0 && hash[1] == 0 && ((hash[2] & 0xF0) == 0))
                    {
                        password += (hash[2] & 0x0F).ToString("x");
                    }

                    index++;
                }

                return password;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Int64 index = 0;
                int count = 0;
                bool[] found = new bool[8] { false, false, false, false, false, false, false, false };
                char[] password = new char[8] { '0', '0', '0', '0', '0', '0', '0', '0'};
                string key = Aoc.Framework.Input.GetString(this);
                while (count < 8)
                {
                    byte[] hash = Md5.Compute(key, index);
                    if (hash[0] == 0 && hash[1] == 0 && ((hash[2] & 0xF0) == 0))
                    {
                        int position = hash[2] & 0x0F;
                        if (position < 8 && !found[position])
                        {
                            found[position] = true;
                            password[position] = (hash[3] & 0xF0).ToString("x")[0];
                            count++;
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