using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aoc.Common;

namespace Aoc
{
    public class Day201605 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201605()
        {
            Codename = "2016-05";
            Name = "How About a Nice Game of Chess?";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Int64 index = 0;
                string password = "";
                string key = Aoc.Framework.Input.GetString(this);
                using (MD5 md5Hash = MD5.Create())
                {
                    while (password.Length < 8)
                    {
                        string hash = ComputeMD5(md5Hash, key + index.ToString());
                        if (hash.StartsWith("00000"))
                        {
                            password += hash[5];
                        }
                        index++;
                    }
                }

                return password;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Int64 index = 0;
                int count = 0;
                bool[] found = new bool[8] {false, false, false, false, false, false, false, false};
                char[] password = new char[8] {'0', '0', '0', '0', '0', '0', '0', '0'};
                string key = Aoc.Framework.Input.GetString(this);
                using (MD5 md5Hash = MD5.Create())
                {
                    while (count < 8)
                    {
                        string hash = ComputeMD5(md5Hash, key + index.ToString());
                        if (hash.StartsWith("00000"))
                        {
                            int position = -1;
                            if (Int32.TryParse(hash.Substring(5, 1), out position))
                            {
                                if (position < 8 && !found[position])
                                {
                                    found[position] = true;
                                    password[position] = hash[6];
                                    count++;
                                }
                            }
                        }
                        index++;
                    }
                }

                return new String(password);
            }

            return "";
        }

        private string ComputeMD5(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

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
    }
}