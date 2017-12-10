using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace _10._Knot
{
    class Knot
    {
        private int[] _knots = null;

        public Knot(int size)
        {
            _knots = new int[size];
            for (int i = 0; i < _knots.Length; ++i)
            {
                _knots[i] = i;
            }
        }

        public String Dump()
        {
            return String.Join(",", _knots.Select(k => k.ToString()));
        }

        public void Twist(int offset, int length)
        {
            int[] after = (int[])_knots.Clone();
            for (int i = 0; i < length; ++i)
            {
                int src = (offset + i) % _knots.Length;
                int dst = (offset + length - i - 1) % _knots.Length;
                after[dst] = _knots[src];
            }
            _knots = after;
        }

        public int GetChecksum()
        {
            return _knots[0] * _knots[1];
        }

        private static string toHex = "0123456789abcdef";

        public string GetDenseHash()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _knots.Length / 16; ++i)
            {
                int v = _knots[i * 16];
                for (int k = 1; k < 16; ++k)
                {
                    v ^= _knots[i * 16 + k];
                }

                int low = v % 16;
                int high = v / 16;
                sb.Append(toHex[high]);
                sb.Append(toHex[low]);
            }

            return sb.
            ToString();
        }
    }
}