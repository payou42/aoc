
namespace Aoc.Common.Numbers
{
    public static class Bezout
    {
        public static (long, long, long) Calculate(long a, long b)
        {
            long u0 = 1, v0 = 0, u1 = 0, v1 = 1, r0 = a, r1 = b;
            while (r1 != 0)
            {
                long r = r0 / r1;
                long r2 = r0 - r * r1;
                long u2 = u0 - r * u1;
                long v2 = v0 - r * v1;
                (r0, u0, v0, r1, u1, v1) = (r1, u1, v1, r2, u2, v2);
            }

            return (r0, u0, v0);
        }
    }
}