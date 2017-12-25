using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Anagram
    {
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