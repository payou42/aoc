using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Aoc
{
    public class KnotHash
    {
        public static byte[] FOOTER = new byte[5] {17, 31, 73, 47, 23};
       
        private int _size;

        private int _rounds;

        private int[] _knots = null;        

        public KnotHash()
        {
            _size = 256;
            _rounds = 64;
            _knots = new int[_size];
        }

        public KnotHash(int size, int rounds)
        {
            _size = size;
            _rounds = rounds;
            _knots = new int[_size];
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

        public int GetSimpleHash()
        {
            return _knots[0] * _knots[1];
        }

        public byte[] GetBytesHash()
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
        
        public string GetDenseHash()
        {
            string toHex = "0123456789abcdef";
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
        
        public void Compute(string input, bool useFooter = true)
        {
            Compute(Encoding.ASCII.GetBytes(input), useFooter);
        }

        public void Compute(byte[] input, bool useFooter = true)
        {
            // Reset knots
            for (int i = 0; i < _knots.Length; ++i)
            {
                _knots[i] = i;
            }

            // Parse input            
            byte[] _lengthes = new byte[input.Length + (useFooter ? FOOTER.Length : 0)];
            for (int i = 0; i < input.Length; ++i)
            {
                _lengthes[i] = input[i];
            }
            if (useFooter)
            {
                for (int i = 0; i < FOOTER.Length; ++i)
                {
                    _lengthes[i + input.Length] = FOOTER[i];
                }
            }

            // Twist and shout
            int current = 0;
            int skip = 0;
            for (int round = 0; round < _rounds; ++round)
            {
                for (int i = 0; i < _lengthes.Length; ++i)
                {
                    Twist(current, _lengthes[i]);
                    current = (current + skip + _lengthes[i]) % _size;
                    skip += 1;
                } 
            }
        }
    }
}