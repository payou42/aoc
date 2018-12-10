using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Strings
{
    public class Repetition
    {
        /// <summary>
        /// Get the first nth in a row repeated character in a string
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <param name="n">The number of repetition wanted</param>
        /// <returns>the repeated character, null otherwise</returns>
        public static char? First(string s, int n)
        {
            char? previous = null;
            int count = 0;
            foreach (char c in s)
            {
                if (previous.HasValue && previous.Value == c)
                {
                    count++;
                    if (count == n)
                    {
                        return previous;
                    }
                }
                else
                {
                    previous = c;
                    count = 1;
                }
            }

            return null;
        }

        /// <summary>
        /// Continuous
        /// </summary>
        /// <param name="a">The string to check</param>
        /// <param name="c">The repeated character</param>
        /// <param name="n">The number of repetition wanted</param>
        /// <returns>true is the string has the wanted repetition</returns>
        public static bool Continuous(string a, char c, int n)
        {
            return a.Contains("".PadLeft(n, c));
        }

        /// <summary>
        /// Group
        /// </summary>
        /// <param name="a">The string to check</param>
        /// <param name="n">The length of the group</param>
        /// <returns>true is the string has the wanted group</returns>
        public static bool Group(string a, int n)
        {
            for (int i = 0; i < a.Length - n; ++i)
            {
                string group = a.Substring(i, n);
                if (a.Substring(i + n).Contains(group))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Spaced
        /// </summary>
        /// <param name="a">The string to check</param>
        /// <param name="n">The length of the space</param>
        /// <returns>true is the string has the wanted repetition</returns>
        public static bool Spaced(string a, int n)
        {
            for (int i = 0; i < a.Length - n - 1; ++i)
            {
                if (a[i] == a[i + 1 + n])
                {
                    return true;
                }
            }
            return false;
        }

        public static int Count(string a, string s)
        {
            int count = 0;
            for (int i = 0; i <= a.Length - s.Length; ++i)
            {
                if (a.Substring(i, s.Length) == s)
                {
                    count++;
                }
            }
            return count;
        }
    }
}