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
        /// Has
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <param name="c">The repeated character</param>
        /// <param name="n">The number of repetition wanted</param>
        /// <returns>true is the string has the wanted repetition</returns>
        public static bool Has(string a, char c, int n)
        {
            return a.Contains("".PadLeft(n, c));
        }
    }
}