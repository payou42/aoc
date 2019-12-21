using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Strings;

namespace Aoc
{
    public class Day201505 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day201505()
        {
            Codename = "2015-05";
            Name = "Doesn't He Have Intern-Elves For This?";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long count = 0;
                string[] forbidden = new string[] { "ab", "cd", "pq", "xy" };
                foreach (string s in _input)
                {
                    int voyels = s.ToCharArray().Count(c => "aeiou".Contains(c));
                    if (voyels < 3)
                    {
                        continue;
                    }

                    char? repeated = Repetition.First(s, 2);
                    if (repeated == null)
                    {
                        continue;
                    }

                    bool wrong = false;
                    foreach (string f in forbidden)
                    {
                        if (s.Contains(f))
                        {
                            wrong = true;
                            break;
                        }
                    }
                    if (wrong)
                    {
                        continue;
                    }

                    count++;
                }

                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long count = 0;
                foreach (string s in _input)
                {
                    if (!Repetition.Group(s, 2))
                    {
                        continue;
                    }

                    if (!Repetition.Spaced(s, 1))
                    {
                        continue;
                    }

                    count++;
                }

                return count.ToString();
            }

            return "";
        }
    }   
}