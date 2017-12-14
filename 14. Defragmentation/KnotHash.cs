using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace _14._Defragmentation
{
    class KnotHash
    {
        public static int SIZE = 256;
        public static int ROUNDS = 64;
        public static byte[] FOOTER = new byte[5] {17, 31, 73, 47, 23};
       
        private int[] _knots = null;        

        public KnotHash()
        {
            _knots = new int[SIZE];
        }

        public String Dump()
        {
            return String.Join(",", _knots.Select(k => k.ToString()));
        }

        private void Twist(int offset, int length)
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

        private byte[] GetBytesHash()
        {
            byte[] hash = new byte[_knots.Length / 16];
            for (int i = 0; i < _knots.Length / 16; ++i)
            {
                int v = _knots[i * 16];
                for (int k = 1; k < 16; ++k)
                {
                    v ^= _knots[i * 16 + k];
                }
                hash[i] = (byte)v;
            }
            return hash;
        }
        
        public byte[] Compute(string str)
        {
            // Reset knots
            for (int i = 0; i < _knots.Length; ++i)
            {
                _knots[i] = i;
            }

            // Parse input
            byte[] fromStr = Encoding.ASCII.GetBytes(str);
            byte[] _lengthes = new byte[fromStr.Length + FOOTER.Length];
            for (int i = 0; i < fromStr.Length; ++i)
            {
                _lengthes[i] = fromStr[i];
            }
            for (int i = 0; i < FOOTER.Length; ++i)
            {
                _lengthes[i + fromStr.Length] = FOOTER[i];
            }            

            // Twist and shout
            int current = 0;
            int skip = 0;
            for (int round = 0; round < ROUNDS; ++round)
            {
                for (int i = 0; i < _lengthes.Length; ++i)
                {
                    Twist(current, _lengthes[i]);
                    current = (current + skip + _lengthes[i]) % SIZE;
                    skip += 1;
                } 
            }

            // Get the hash bytes
            return GetBytesHash();
        }
    }
}