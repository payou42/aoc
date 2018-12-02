using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Strings
{
    public class Difference
    {
        /// <summary>
        /// Count the number of differences between 2 strings
        /// </summary>
        /// <param name="a">The first string</param>
        /// <param name="b">The second string</param>
        /// <returns>the number of differences in the 2 strings</returns>
        public static int Count(string a, string b)
        {
            int diff = Math.Abs(a.Length - b.Length);
            int length = Math.Min(a.Length, b.Length);
            for (int i = 0; i < length; ++i)
            {
                if (a[i] != b[i])
                {
                    diff++;
                }
            }
            return diff;
        }

        /// <summary>
        /// Get the common part between 2 strings, ignoring different characters
        /// </summary>
        /// <param name="a">The first string</param>
        /// <param name="b">The second string</param>
        /// <returns>the common part of the 2 strings</returns>
        public static string GetCommonPart(string a, string b)
        {
            string common = "";
            int length = Math.Min(a.Length, b.Length);
            for (int i = 0; i < length; ++i)
            {
                if (a[i] == b[i])
                {
                    common += a[i];
                }
            }
            return common;
        }
    }
}