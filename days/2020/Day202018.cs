using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202018 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day202018()
        {
            Codename = "2020-18";
            Name = "Operation Order";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            return _input.Sum(s => this.EvaluateExpression(s, 0, part).Result).ToString();
        }

        public (long Result, int OutIndex) EvaluateExpression(string s, int inIndex, Aoc.Framework.Part part)
        {
            int currentIndex = inIndex;
            List<object> tokens = new List<object>();
            while (currentIndex < s.Length && s[currentIndex] != ')')
            {
                if (s[currentIndex] == '(')
                {
                    long res;
                    (res, currentIndex) = this.EvaluateExpression(s, currentIndex + 1, part);
                    tokens.Add(res);
                    continue;
                }

                if (s[currentIndex] == '+' || s[currentIndex] == '*')
                {
                    tokens.Add(s[currentIndex].ToString());
                }

                else if (char.IsDigit(s[currentIndex]))
                {
                    tokens.Add(long.Parse(s[currentIndex].ToString()));
                }

                currentIndex++;
            }

            // Resolve pending operations
            if (part == Aoc.Framework.Part.Part1)
            {
                // Process operations from left to right
                while (tokens.Count > 1)
                {
                    long a = (long)tokens[0];
                    long b = (long)tokens[2];
                    string op = (string)tokens[1];
                    tokens.RemoveAt(2);
                    tokens.RemoveAt(1);
                    tokens[0] = op == "*" ? a * b : a + b;
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Process pending additions first, then multiplications
                int i = tokens.IndexOf("+");
                while (i != -1)
                {
                    long a = (long)tokens[i - 1];
                    long b = (long)tokens[i + 1];
                    tokens.RemoveAt(i + 1);
                    tokens.RemoveAt(i);
                    tokens[i - 1] = a + b;
                    i = tokens.IndexOf("+");
                }

                i = tokens.IndexOf("*");
                while (i != -1)
                {
                    long a = (long)tokens[i - 1];
                    long b = (long)tokens[i + 1];
                    tokens.RemoveAt(i + 1);
                    tokens.RemoveAt(i);
                    tokens[i - 1] = a * b;
                    i = tokens.IndexOf("*");
                }
            }

            return (tokens.Count == 0 ? 0L : (long)tokens[0] , currentIndex + 1);
        }
    }   
}