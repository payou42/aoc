using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201609 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _compressed;

        public Day201609()
        {
            Codename = "2016-09";
            Name = "Explosives in Cyberspace";
        }

        public void Init()
        {
            _compressed = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            return DecompressLength(_compressed, part).ToString();
        }

        private Int64 DecompressLength(string compressed, Aoc.Framework.Part part)
        {
            int cursor = 0;
            Int64 length = 0;
            while (cursor < compressed.Length)
            {
                if (compressed[cursor] == '(')
                {
                    int end = compressed.IndexOf(')', cursor);
                    int[] repeat = compressed.Substring(cursor + 1, end - cursor - 1).Split("x").Select(item => Int32.Parse(item)).ToArray();
                    if (part == Aoc.Framework.Part.Part1)
                    {
                        length += repeat[0] * repeat[1];
                    }
                    else
                    {
                        string sub = compressed.Substring(end + 1, repeat[0]);
                        length += DecompressLength(sub, part) * repeat[1];
                    }
                    cursor = end + repeat[0] + 1; 
                }
                else
                {
                    cursor++;
                    length++;
                }
            }
            return length;
        }
    }
}