using System.Collections.Generic;
using System.Linq;

namespace Aoc.Common.Numbers
{
    public static class ChineseCongruence
    {
        public static long Calculate(List<(long Remainder, long Modulus)> input)
        {
            // Lets use a little math here
            long n = input.Select(v => v.Modulus).Aggregate((v1, v2) => v1 * v2);
            long r = input.Select(ni => ((ni.Remainder % ni.Modulus) * GetE(ni.Modulus, n)) % n).Sum() % n;
            return r;
        }

        private static long GetE(long ni, long n)
        {
            long nib = n / ni;
            (long pgcd, long ui, long vi) = Bezout.Calculate(ni, nib);
            return (n + (vi * nib)) % n;
        }
    }
}