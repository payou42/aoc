using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201904 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public int[] _range;

        public Day201904()
        {
            Codename = "2019-04";
            Name = "Secure Container";
        }

        public void Init()
        {
            _range = Aoc.Framework.Input.GetIntVector(this, "-");
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int count = Enumerable.Range(_range[0], _range[1] - _range[0]).Count(n => IsValid1(n));
                return count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int count = Enumerable.Range(_range[0], _range[1] - _range[0]).Count(n => IsValid2(n));
                return count.ToString();
            }

            return "";
        }

        private bool IsValid1(int n)
        {
            bool hasDouble = false;
            char[] digits = n.ToString().ToCharArray();
            for (int i = 1; i < digits.Length; ++i)
            {
                if (digits[i] < digits[i - 1])
                {
                    return false;
                }

                if (digits[i] == digits[i - 1])
                {
                    hasDouble = true;
                }
            }

            return hasDouble;
        }

        private bool IsValid2(int n)
        {
            bool hasDouble = false;
            int repetition = 0;
            char[] digits = n.ToString().ToCharArray();
            for (int i = 1; i < digits.Length; ++i)
            {
                if (digits[i] < digits[i - 1])
                {
                    return false;
                }

                if (digits[i] == digits[i - 1])
                {
                    repetition++;
                }
                else
                {
                    if (repetition == 1)
                    {
                        hasDouble = true;
                    }

                    repetition = 0;
                }
            }

            return hasDouble || (repetition == 1);
        }
    }   
}