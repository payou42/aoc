using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if 2 strings are anagrams (a different sequence of the same letters)
        /// </summary>
        /// <param name="a">The first string</param>
        /// <param name="b">The second string</param>
        /// <returns>true if a and b are anagrams</returns>
        public static bool IsAnagram(this String a, String b)
        {
            for (int i = 'a'; i <= 'z'; ++i)
            {
                int ca = 0;
                int cb = 0;
                for (int ia = 0; ia < a.Length; ++ia)
                {
                    if (a[ia] == i)
                    {
                        ca++;
                    }
                }

                for (int ib = 0; ib < b.Length; ++ib)
                {
                    if (b[ib] == i)
                    {
                        cb++;
                    }
                }

                if (ca != cb)
                {
                    return false;
                }
            }

            return true;
        }

        public static String Reverse(this String input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static String ReplaceLetter(this String input, char x, char y)
        {
            return input.Replace(x, '.').Replace(y, x).Replace('.', y);
        }

        public static String ReplacePosition(this String input, int x, int y)
        {
            int a = Math.Min(x, y);
            int b = Math.Max(x, y);
            return input[..a] + input[b] + input[(a + 1)..b] + input[a] + input[(b + 1)..];
        }

        public static String ReversePart(this String input, int x, int y)
        {
            return input[..x] + input[x..(y + 1)].Reverse() + input[(y + 1)..];
        }

        public static String Splice(this String input, int start, int length, string replacement = "")
        {
            int x = start;
            int y = x + length - 1;
            return input[..x] + replacement + input[(y + 1)..];
        }

        public static String MovePart(this String input, int from, int to)
        {
            string s = input[from..(from + 1)];
            return input.Remove(from, 1).Insert(to, s);            
        }

        public static String RotateLeft(this String input, int amount)
        {
            int n = amount % input.Length;
            if (n == 0)
            {
                return input;
            }

            return input[n..] + input[..n];
        }

        public static String RotateRight(this String input, int amount)
        {
            int n = amount % input.Length;
            if (n == 0)
            {
                return input;
            }

            return input[^n..] + input[..^n];
        }

        public static String RotateLetter(this String input, char letter)
        {
            int n = input.IndexOf(letter);
            n = ((n >= 4 ? 2 : 1) + n) % input.Length;
            if (n == 0)
            {
                return input;
            }
    
            return input[^n..] + input[..^n];
        }
    }
}