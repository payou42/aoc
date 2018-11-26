using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Strings
{
    public class Anagram
    {
        /// <summary>
        /// Check if 2 strings are anagrams (a different sequence of the same letters)
        /// </summary>
        /// <param name="a">The first string</param>
        /// <param name="b">The second string</param>
        /// <returns>true if a and b are anagrams</returns>
        public static bool Check(string a, string b)
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
    }
}