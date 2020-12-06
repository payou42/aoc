using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202006 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[][] _input;

        public Day202006()
        {
            Codename = "2020-06";
            Name = "Custom Customs";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringMatrix(this, "\r\n", "\r\n\r\n");
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long count = 0;
                foreach (var group in _input)
                {
                    HashSet<char> answers = new HashSet<char>();
                    foreach (var people in group)
                    {
                        foreach (var answer in people)
                        {
                            answers.Add(answer);
                        }
                    }

                    count += answers.Count;
                }

                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long count = 0;
                foreach (var group in _input)
                {
                    for (char c = 'a'; c <= 'z'; ++c)
                    {
                        if (group.All(s => s.Contains(c)))
                        {
                            count++;
                        }
                    } 
                }

                return count.ToString();
            }

            return "";
        }
    }   
}