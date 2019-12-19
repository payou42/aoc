using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using System.Text.RegularExpressions;

namespace Aoc
{
    public class Day201508 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day201508()
        {
            Codename = "2015-08";
            Name = "Matchsticks";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _input.Sum(s => 2 + (s.Length - GetUnescapedLength(s))).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _input.Sum(s => 2 + (GetEscapedLength(s) - s.Length)).ToString();
            }

            return "";
        }

        private int GetUnescapedLength(string s)
        {
            int diff = 0;
            Stack<char> queue = new Stack<char>();
            char[] data = s.ToCharArray();
            for (int i = 0; i < data.Length; ++i)
            {
                if ((data[i] == '"') || (data[i] == '\\') || (data[i] == 'x'))
                {
                    if ((queue.TryPeek(out var c)) && (c == '\\'))
                    {
                        queue.Pop();
                        queue.Push('.');
                        diff += 1;

                        if (data[i] == 'x')
                        {
                            diff += 2;
                            i += 2;
                        }

                        continue;
                    }
                }

                queue.Push(data[i]);
            }
            
            return s.Length - diff;
        }

        private int GetEscapedLength(string s)
        {
            int diff = 0;
            char[] data = s.ToCharArray();
            for (int i = 0; i < data.Length; ++i)
            {
                if ((data[i] == '"') || (data[i] == '\\'))
                {
                    diff++;
                }
            }            
            return s.Length + diff;
        }
    }   
}