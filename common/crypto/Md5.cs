using System;
using System.Security.Cryptography;
using System.Text;

namespace Aoc.Common.Crypto
{
    public static class Md5
    {
        private static readonly MD5 _md5;

        static Md5()
        {
            _md5 = MD5.Create();
        }

        public static byte[] Compute(string s)
        {
            // Convert the input string to a byte array and compute the hash.
            return _md5.ComputeHash(Encoding.UTF8.GetBytes(s));
        }

        public static byte[] Compute(string salt, long index)
        {
            return Compute(salt + index.ToString());
        }

        public static string ComputeString(string s)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = _md5.ComputeHash(Encoding.UTF8.GetBytes(s));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string ComputeString(string salt, long index)
        {
            return ComputeString(salt + index.ToString());
        }
    }
}
