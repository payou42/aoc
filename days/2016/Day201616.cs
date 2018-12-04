using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201616 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201616()
        {
            Codename = "2016-16";
            Name = "Dragon Checksum";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return GenerateHash(272);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return GenerateHash(35651584);
            }

            return "";
        }

        private string Iterate(string s)
        {
            var t = string.Join("", s.Select(c => c == '1' ? '0' : '1').Reverse());
            return s + '0' + t;
        }

        private string GenerateHash(int length)
        {
            // Generate a long enough string
            byte[] data = new byte[length];
            for (int i = 0; i < _input.Length; ++i)
            {
                data[i] = byte.Parse(_input[i].ToString()); 
            }
            
            // Build the string
            long offset = 0;

            // Do the job
            for (long index = _input.Length; index < length; ++index)
            {
                if (offset == 0)
                {
                    data[index] = 0;
                    offset = index;
                }
                else
                {
                    data[index] = (byte)(1 - data[offset - 1]);
                    offset--;
                }
            }            

            // Generate the hash
            while (data.Length % 2 == 0)
            {
                byte[] hash = new byte[data.Length / 2];
                for (int i = 0; i < hash.Length; ++i)
                {
                    if (data[2 * i] == data[2 * i + 1])
                    {
                        hash[i] = 1;
                    }
                    else
                    {
                        hash[i] = 0;
                    }
                }
                data = hash;
            }

            return string.Join("", data.Select(c => c.ToString()));
        }
    }   
}